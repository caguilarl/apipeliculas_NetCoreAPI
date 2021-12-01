using ApiPeliculas.Models;
using ApiPeliculas.Models.Dtos;
using ApiPeliculas.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Controllers
{
    [Route("api/[controller]")]//Esto es lo mismo que [Route("api/Categorias")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _icategoriaRepository;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository icategoriaRepository, IMapper mapper)
        {
            _icategoriaRepository = icategoriaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetCategorias()
        {
            var listaCategorias = _icategoriaRepository.GetCategorias();
            var listaCategoriasDTO = new List<CategoriaDTO>();

            foreach (var categoria in listaCategorias) {
                listaCategoriasDTO.Add(_mapper.Map<CategoriaDTO>(categoria));
            }

            return Ok(listaCategoriasDTO);
        }

        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        public ActionResult GetCategoria(int categoriaId)
        {
            var itemCategoria = _icategoriaRepository.GetCategoria(categoriaId);

            if (itemCategoria == null) {
                return NotFound();
            }

            var itemCategoriaDTO = _mapper.Map<CategoriaDTO>(itemCategoria);
            return Ok(itemCategoriaDTO);
        }

        [HttpPost]
        public ActionResult CreateCategoria([FromBody] CategoriaDTO categoriaDTO)
        {
            if(categoriaDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_icategoriaRepository.CategoriaExists(categoriaDTO.Nombre))
            {
                ModelState.AddModelError("", "La categoria ya existe");
                return StatusCode(404, ModelState);
            }

            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            if (!_icategoriaRepository.CreateCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal creando la categoria ${categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id }, categoria );
        }

        [HttpPatch("{categoriaId:int}", Name = "UpdateCategoria")]
        public ActionResult UpdateCategoria(int categoriaId, CategoriaDTO categoriaDTO)
        {
            if(categoriaDTO == null || categoriaDTO.Id != categoriaId)
            {
                return BadRequest(ModelState);
            }

            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            if (!_icategoriaRepository.UpdateCategoria(categoria)) {
                ModelState.AddModelError("", $"Algo salió mal actualizando la categoría ${categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{categoriaId:int}")]
        public ActionResult DeleteCategoria(int categoriaId)
        {
            if (!_icategoriaRepository.CategoriaExists(categoriaId))
            {
                return NotFound();
            }

            var categoria = _icategoriaRepository.GetCategoria(categoriaId);

            if (!_icategoriaRepository.DeleteCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando la categoria ${categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
