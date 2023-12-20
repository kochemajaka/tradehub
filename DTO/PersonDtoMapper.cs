using WebAPI_6_01.Domain;

namespace WebAPI01.API.Dto
{
    public static class PersonDtoMapper
    {
        public static PersonDto ToDto(Person person)
        {
            var personDto = new PersonDto
            { 
                Id = person.Id, 
                Name = person.Name 
            };

            foreach(var phone in person.Phones)
            {
                personDto.PhoneDtos.Add(PhoneDtoMapper.ToDto(phone));
            }

            return personDto;
        }

        public static Person ToEntity(PersonDto personDto)
        {
            var person = new Person
            {
                Id = personDto.Id,
                Name = personDto.Name
            };

            foreach(var phoneDto in personDto.PhoneDtos)
            {
                var phone = PhoneDtoMapper.ToEntity(phoneDto);
                phone.Person = person;
                phone.PersonId = person.Id;
                person.AddPhone(phone);
            }

            return person;
        }

        public static List<PersonDto> ToDto(List<Person> persons)
        {
            var personDtos = new List<PersonDto>();
            foreach(var person in persons)
            {
                personDtos.Add(ToDto(person));
            }

            return personDtos;
        }
    }
}
