
using Xunit;
using Model;
using ExpectedObjects;
using System;
using _Util;
using Xunit.Abstractions;
using _Builder;
using Bogus;

namespace test
{
    public class CursoTest : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly int _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public CursoTest(ITestOutputHelper output){
            _output = output;
            _output.WriteLine("Construtor sendo executado");
            var faker = new Faker();

            _nome = faker.Random.Word();
            _cargaHoraria = faker.Random.Int(1,100);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = faker.Random.Double(50,1000);
            _descricao = faker.Lorem.Paragraph();
            


        }

        public void Dispose(){
            _output.WriteLine("Dispose sendo executado");
        }

        [Fact]
        public void DeveCriarCurso()
        {
        //Given
        var cursoEsperado = new{
            Nome = "Informática básica",
            CargaHoraria = 80,
            PublicoAlvo = PublicoAlvo.Estudante,
            Valor = (double) 950,
            Descricao = _descricao
        };


        //When
        var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);
        
        //Then
        cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido){

            Assert.Throws<ArgumentException>(() =>
             CursoBuilder.Novo().ComNome(nomeInvalido).Build()).ComMensagem("Nome inválido");            
        } 
        
        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorQue1(int cargaHorariaInvalida){

            Assert.Throws<ArgumentException>(() =>
              CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build()).ComMensagem("Carga horária inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerValorMenorQue1(double valorInvalido){

            Assert.Throws<ArgumentException>(() =>
            CursoBuilder.Novo().ComValor(valorInvalido).Build()).ComMensagem("Valor inválido");
        }
    }
}