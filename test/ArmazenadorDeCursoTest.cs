using _Builder;
using _Util;
using Bogus;
using Model;
using Moq;
using System;
using Xunit;

namespace test
{
    public class ArmazenadorDeCursoTest{
        private readonly CursoDto _cursoDto;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;

        //Setup
        public ArmazenadorDeCursoTest()
        {
            var fake = new Faker();
            _cursoDto = new CursoDto
            {
                Nome = fake.Random.Words(),
                Descricao = fake.Lorem.Paragraph(),
                CargaHoraria = fake.Random.Int(50,100),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(1000,2000)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
        }
        [Fact]
        public void DeveAdicionarCurso()
        {
            

            _armazenadorDeCurso.Armazenar(_cursoDto);
            //Exemplo - Uso de atLeast em chamadas for
            _cursoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Curso>(
                    c => c.Nome==_cursoDto.Nome
                    //c => CursoDto.compara(c, _cursoDto)
                    )
            ),Times.AtLeast(1));

            /*cursoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Curso>(
                    c => c.Nome == cursoDto.Nome
                    )
            ), Times.Never);*/
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Nome do curso já consta no banco de dados");
        }
        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Medico";
            _cursoDto.PublicoAlvo = "Medico";

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Publico Alvo inválido");

        }

        
    }
    

}
