///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp.Deserializers;
using System.Collections.Generic;

namespace ReceptionSdk.Models
{
    class GenericResponse<T>
    {
        [DeserializeAs(Name = "offset")]
        public int Offset { get; set; }

        [DeserializeAs(Name = "limit")]
        public int Limit { get; set; }

        [DeserializeAs(Name = "total")]
        public int Total { get; set; }

        [DeserializeAs(Name = "data")]
        public List<T> Data { get; set; }

        public GenericResponse() : base()
        {
        }
    }
}
