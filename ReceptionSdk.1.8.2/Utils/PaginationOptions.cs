///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
namespace ReceptionSdk.Utils
{
    public class PaginationOptions
    {
        private int offset = 0;
        private int limit = 15;

        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public int Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        public static PaginationOptions Create()
        {
            return new PaginationOptions();
        }

        public PaginationOptions Next()
        {
            Offset = Offset + Limit;
            return this;
        }

        public PaginationOptions WithOffset(int offset)
        {
            Offset = offset;
            return this;
        }

        public PaginationOptions WithLimit(int limit)
        {
            Limit = limit;
            return this;
        }
    }
}
