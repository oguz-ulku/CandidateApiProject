using static KPSPublic.KPSPublicSoapClient;

namespace CandidateApiProject.Services
{
    public class TCKNAuthenticationService
    {

        public Task<bool> CheckIdentityNo(string identityNo, string name, string surname, DateTime birthDate)
        {
            var result = false;
            try
            {
                var client = new KPSPublic.KPSPublicSoapClient(EndpointConfiguration.KPSPublicSoap);
                var checkIdentityNoResponse = client.TCKimlikNoDogrulaAsync(long.Parse(identityNo), name, surname, birthDate.Year).GetAwaiter().GetResult();
                result = checkIdentityNoResponse.Body.TCKimlikNoDogrulaResult;
            }
            catch{
                result = false;
            }

            return Task.FromResult(result);
        }
    }
}
