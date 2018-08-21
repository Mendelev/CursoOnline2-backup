using System;

namespace Model
{
    public class Curso
    {
        public Curso(string nome, string descricao, int cargaHoraria, PublicoAlvo publicoAlvo, double valor){

            if(string.IsNullOrEmpty(nome)){
                throw new ArgumentException("Nome inválido");
            }
            if(cargaHoraria<1){
                throw new ArgumentException("Carga horária inválida");
            }
            if(valor<1){
                throw new ArgumentException("Valor inválido");
            }
            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
            Descricao = descricao;

        }

        public string Nome {get; private set;}
        public int CargaHoraria {get; private set;}
        public PublicoAlvo PublicoAlvo {get; private set;}
        public double Valor {get; private set;}
        
        public string Descricao {get; private set;}

    }
    public enum PublicoAlvo{
            Estudante,
            Universitario,
            Empregado,
            Empreendedor
        }
}
