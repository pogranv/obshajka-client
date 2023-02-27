﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obshajka.Models;
using Obshajka.Mocks;

namespace Obshajka.ObshajkaWebApi
{
    public static class ObshajkaApi
    {
        public static long AuthorizeUser(string email, string password)
        {
            // Mock
            return 1;
        }

        public static List<Advertisement> GetAdvertisements()
        {
            return MocksClass.GetAdvertisements_Mock();
        }
    }
}