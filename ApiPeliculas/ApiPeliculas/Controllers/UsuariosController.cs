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
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _userRepo;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _userRepo = usuarioRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetUsuarios()
        {
            var listaUsuarios = _userRepo.GetUsuarios();
            var listaUsuariosDTO = new List<UsuarioDTO>();

            foreach(var usuario in listaUsuarios)
            {
                listaUsuariosDTO.Add(_mapper.Map<UsuarioDTO>(usuario));
            }

            return Ok(listaUsuariosDTO);
        }

        [HttpGet("{usuarioId:int}", Name = "GetUsuario")]
        public ActionResult GetUsuario(int usuarioId)
        {
            var usuario = _userRepo.GetUsuario(usuarioId);

            if(usuario == null){
                return NotFound();
            }

            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);

            return Ok(usuarioDTO);
        }


    }
}
