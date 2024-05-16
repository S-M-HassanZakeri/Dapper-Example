using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Layer.Common.Enumerate.Enumerate;

namespace Layer.Common.ErrorExaptation
{
    public class ResultStatusOpration
    {
        public ResultStatusOpration(string? title = "", string? message = "", ResultStatusEnum? typeStatus = ResultStatusEnum.info)
        {
            Title = title;
            Message = message;
            TypeStatus = typeStatus.Value;
            ShowAleart = Title;
            ResultStatus = typeStatus.Value;
        }
        private string _firstProperty;
        private ResultStatusEnum _resultStutis;
        public string FirstProperty
        {
            get { return _firstProperty; }
            set
            {
                _firstProperty = value;
                SecondProperty = _firstProperty; // انتساب مقدار به پراپرتی دوم
            }
        }
        public string SecondProperty { get; set; }
        public string? Message { get; set; }
        public string Title
        {
            get
            {
                return _firstProperty;
            }
            set
            {
                _firstProperty = value;
                ShowAleart = _firstProperty;
            }
        }



        public ResultStatusEnum TypeStatus
        {
            get
            {
                return _resultStutis;
            }
            set
            {
                _resultStutis = value;
                ResultStatus = _resultStutis;
            }
        }


        public static string? ShowAleart { get; set; }
        public static ResultStatusEnum ResultStatus { get; set; }
        public static bool IsEmptiy { get; set; }

        public ResultStatusOpration FiilMe()
        {
            var result = new ResultStatusOpration()
            {
                Message = ShowAleart,
            };
            return result;
        }





    }
}
