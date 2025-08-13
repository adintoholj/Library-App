using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliothequariaFrontend.Services
{
    class AuthService
    {
        private const string BaseUrl =
#if ANDROID
    "http://10.0.2.2:5000/api/Radnik";   
#else
    "https://localhost:5001/api/Radnik"; // Windows/macOS desktop
#endif
    }
}
