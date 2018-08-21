using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
        Curso ObterPeloNome(string nome);
    }
    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;
        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var cursoJaSalvo = _cursoRepositorio.ObterPeloNome(cursoDto.Nome);

            if (cursoJaSalvo != null)
            {
                throw new ArgumentException("Nome do curso já consta no banco de dados");
            }

            if (!Enum.TryParse<PublicoAlvo>(cursoDto.PublicoAlvo, out var publicoAlvo))
            {
                throw new ArgumentException("Publico Alvo inválido");
            }


            var curso =
                new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria, publicoAlvo, cursoDto.Valor);

            _cursoRepositorio.Adicionar(curso);
        }
    }

    public class CursoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public double Valor { get; set; }

        public static bool compara(Curso c1, CursoDto c2)
        {
            return ((c1.Nome == c2.Nome) && (c1.Descricao == c2.Descricao) && (c1.CargaHoraria == c2.CargaHoraria) && (c1.Valor == c2.Valor));
        }
    }
}
