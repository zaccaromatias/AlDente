using System.Collections.Generic;

namespace AlDente.Contracts.Core
{
    public class BasicResultDTO<T>
    {
        BasicResultDTO _result;
        public ResultState State => _result.State;
        public List<string> Errors => _result.Errors;
        public string AllErrors => _result.AllErrors;
        public string MessageSuccsefully => _result.MessageSuccsefully;

        public bool IsValid => _result.IsValid;
        public T Data { get; private set; }


        private BasicResultDTO(T data, BasicResultDTO basicResultDTO)
        {
            Data = data;
            _result = basicResultDTO;
        }
        public string ConcatErrors(string separator)
        {
            return _result.ConcatErrors(separator);
        }
        public static BasicResultDTO<T> Failled(string error)
        {
            return new BasicResultDTO<T>(default, BasicResultDTO.Failled(error));
        }
        public static BasicResultDTO<T> Failled(T data, string error)
        {
            return new BasicResultDTO<T>(data, BasicResultDTO.Failled(error));
        }
        public static BasicResultDTO<T> Success(T data)
        {
            return new BasicResultDTO<T>(data, BasicResultDTO.Success());
        }

        public static BasicResultDTO<T> Success(T data, string messageSuccessfuly)
        {
            return new BasicResultDTO<T>(data, BasicResultDTO.Success(messageSuccessfuly));
        }
    }
    public class BasicResultDTO
    {
        public bool IsValid => this.State == ResultState.Success;
        public ResultState State { get; private set; }

        public string MessageSuccsefully { get; private set; }
        public List<string> Errors { get; private set; }

        public string AllErrors => ConcatErrors("<br/>");

        public string ConcatErrors(string separator)
        {
            return string.Join(separator, this.Errors);
        }

        private BasicResultDTO(ResultState state, List<string> errors) : this(state)
        {
            this.Errors = errors ?? new List<string>();
        }
        private BasicResultDTO()
        {
            this.Errors = new List<string>();
        }
        private BasicResultDTO(ResultState state) : this()
        {
            this.State = state;

        }
        private BasicResultDTO(ResultState state, string messageSuccesfuly) : this(state)
        {
            this.MessageSuccsefully = messageSuccesfuly;

        }



        public static BasicResultDTO Failled(string error)
        {
            return new BasicResultDTO(ResultState.Error, new List<string> { error });
        }
        public static BasicResultDTO Success(string message)
        {
            return new BasicResultDTO(ResultState.Success, message);
        }
        public static BasicResultDTO Success()
        {
            return new BasicResultDTO(ResultState.Success);
        }


    }
    public enum ResultState
    {
        Error,
        Success
    }
}
