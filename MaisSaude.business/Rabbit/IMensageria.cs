namespace MaisSaude.Business.Rabbit
{
    public interface IMensageria
    {
        void EnviarMensagemRabbit(object conteudo, string exchange = "", string fila = "");


    }
}
