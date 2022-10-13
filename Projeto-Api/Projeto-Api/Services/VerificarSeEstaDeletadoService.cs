namespace Projeto_Api.Services
{
    public class VerificarSeEstaDeletadoService
    {

        public static bool VerificarSeEstaDeletado(DateTime deletedOn)
        {
            DateTime tempoParaVerificarSeFoiDeletado = new DateTime(2000, 01, 01);
            return (!(deletedOn < tempoParaVerificarSeFoiDeletado));
        }
    }
}
