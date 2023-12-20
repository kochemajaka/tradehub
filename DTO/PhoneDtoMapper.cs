using WebAPI_6_01.Domain;

namespace WebAPI01.API.Dto
{
    public static class PhoneDtoMapper
    {
        public static PhoneDto ToDto(Phone phone)
        {
            var phoneDto = new PhoneDto
            {
                Id = phone.Id,
                PhoneNumber = phone.PhoneNumber,
                PhoneType = phone.PhoneType
            };
            return phoneDto;
        }

        public static Phone ToEntity(PhoneDto phoneDto)
        {
            var phone = new Phone
            {
                Id = phoneDto.Id,
                PhoneNumber = phoneDto.PhoneNumber,
                PhoneType = phoneDto.PhoneType
            };
            return phone;
        }
    }
}
