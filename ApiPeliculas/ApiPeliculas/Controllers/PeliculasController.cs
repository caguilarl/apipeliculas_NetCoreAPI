using ApiPeliculas.Models;
using ApiPeliculas.Models.Dtos;
using ApiPeliculas.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Controllers
{
    [Route("api/Peliculas")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaRepository _peliculaRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        public PeliculasController(IPeliculaRepository peliculaRepository, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _peliculaRepository = peliculaRepository;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public ActionResult GetPeliculas()
        {
            var peliculas = _peliculaRepository.GetPeliculas();
            var peliculasDTO = new List<PeliculaDTO>();

            foreach (var pelicula in peliculas)
            {
                peliculasDTO.Add(_mapper.Map<PeliculaDTO>(pelicula));
            }

            return Ok(peliculas);
        }

        [HttpGet("GetPeliculasEnCategoria/{categoriaId:int}")]
        public ActionResult GetPeliculasEnCategoria(int categoriaId)
        {
            var peliculas = _peliculaRepository.GetPeliculasEnCategoria(categoriaId);

            if(peliculas == null)
            {
                return NotFound();
            }

            var peliculasDTO = new List<PeliculaDTO>();

            foreach(var pelicula in peliculas)
            {
                peliculasDTO.Add(_mapper.Map<PeliculaDTO>(pelicula));
            }

            return Ok(peliculasDTO);
        }

        [HttpGet("SearchPeliculas")]
        public ActionResult SearchPeliculas(string nombre)
        {
            try
            {
                var peliculas = _peliculaRepository.SearchPelicula(nombre);

                if (peliculas.Any())
                {
                    return Ok(peliculas);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos de la aplicación");
            }
        }

        [HttpGet("{peliculaId:int}", Name = "GetPelicula")]
        public ActionResult GetPelicula(int peliculaId)
        {
            var itemPelicula = _peliculaRepository.GetPelicula(peliculaId);

            if(itemPelicula == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PeliculaDTO>(itemPelicula));
        }

        [HttpPost]
        public ActionResult CreatePelicula([FromForm] PeliculaCreateDTO peliculaCreateDTO)
        {
            if(peliculaCreateDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_peliculaRepository.PeliculaExists(peliculaCreateDTO.Nombre))
            {
                ModelState.AddModelError("", $"La película ya existe");
                return StatusCode(404, ModelState);
            }

            /*Subida de archivos*/
            var imagen = peliculaCreateDTO.Foto;
            string rutaPrincipal = _hostEnvironment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;

            if(imagen.Length > 0)
            {
                //Nueva imagen
                var nombreFoto = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal, @"Images");
                var extension = Path.GetExtension(archivos[0].FileName);

                using var fileStreams = new FileStream(Path.Combine(subidas, nombreFoto + extension), FileMode.Create);
                archivos[0].CopyTo(fileStreams);

                peliculaCreateDTO.RutaImagen = @"Images\" + nombreFoto + extension;
            }


            var pelicula = _mapper.Map<Pelicula>(peliculaCreateDTO);

            if(!_peliculaRepository.CreatePelicula(pelicula))
            {
                ModelState.AddModelError("", $"Algo salió mal creando la película ${peliculaCreateDTO.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPelicula", new { peliculaId = pelicula.Id }, _mapper.Map<PeliculaDTO>(pelicula));
        }

        [HttpPatch("{peliculaId:int}", Name = "UpdatePelicula")]
        public ActionResult UpdatePelicula(int peliculaId, PeliculaUpdateDTO peliculaUpdateDTO)
        {
            if(peliculaUpdateDTO == null || peliculaUpdateDTO.Id != peliculaId)
            {
                return BadRequest(ModelState);
            }

            var pelicula = _mapper.Map<Pelicula>(peliculaUpdateDTO);

            if(!_peliculaRepository.UpdatePelicula(pelicula))
            {
                ModelState.AddModelError("", $"Algo salió mal al actualizar la película ${pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{peliculaId:int}")]
        public ActionResult DeletePelicula(int peliculaId)
        {
            if (!_peliculaRepository.PeliculaExists(peliculaId))
            {
                return NotFound();
            }

            var pelicula = _peliculaRepository.GetPelicula(peliculaId);

            if (!_peliculaRepository.DeletePelicula(pelicula))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando la película ${pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
