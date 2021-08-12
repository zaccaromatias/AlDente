namespace AlDente.Contracts.Reservas
{
    public class ReservaResult : IReservaResult
    {
        public string Codigo { get; set; }
        public string Error { get; private set; }

        public ReservaResultState State { get; private set; }


        private ReservaResult(ReservaResultState state, string codigo, string error)
        {
            State = state;
            Codigo = codigo;
            Error = error;
        }

        public static ReservaResult Failled(string error)
        {
            return new ReservaResult(ReservaResultState.Error, null, error);
        }
        public static ReservaResult Success(string codigo)
        {
            return new ReservaResult(ReservaResultState.Success, codigo, null);


        }
    }
    public interface IReservaResult
    {
        public string Codigo { get; }

        public ReservaResultState State { get; }

        public string Error { get; }


    }

    public enum ReservaResultState
    {
        Error,
        Success
    }
}
