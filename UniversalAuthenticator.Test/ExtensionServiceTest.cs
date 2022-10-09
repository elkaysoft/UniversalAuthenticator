using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Extensions;
using Xunit;

namespace UniversalAuthenticator.Test
{
    public class ExtensionServiceTest
    {
        [Fact]
        public void AlphaNumeric_ShouldNotBeEmpty()
        {
            var data = ExtensionsService.GenerateAlphaNumeric(10);
            Assert.NotEmpty(data);
        }

        [Fact]
        public bool AlphaNumericLenght_ShouldBeValid()
        {            
            var data = ExtensionsService.GenerateAlphaNumeric(12);            
            if (data.Length != 12)
                return false;
            else
                return true;        
        }

        [Fact]
        public void ShouldEncodeText_Successfully()
        {
            string expected = "T2xhbWlsZWthbg==";
            string actual = ExtensionsService.Encode("Olamilekan");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldDecodeText_Successfully()
        {
            string expected = "Olamilekan";

            string actual = ExtensionsService.Decode("T2xhbWlsZWthbg==");
            Assert.Equal(expected, actual);
        }

    }
}
