using AutoMapper;
using CardTask.Application.Authorization;
using CardTask.Application.Dtos;
using CardTask.Application.Helpers;
using CardTask.Core.Entities;
using CardTask.DataAccess.Persistence;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTask.Application.Services
{
    public interface ICardService
    {
        IEnumerable<CardResponse> GetAll();
        CardResponse GetById(int id);
        CardResponse Create(CardCreateRequest model, string userName);
        CardResponse Update(int id, CardUpdateRequest model);
        void Delete(int id);
    }
    public class CardService : ICardService
    {
        private readonly CardDbContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public CardService(
            CardDbContext context,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        public IEnumerable<CardResponse> GetAll()
        {
            var Cards = _context.Cards;
            return _mapper.Map<IList<CardResponse>>(Cards);
        }
        private Card GetCard(int id)
        {
            var Card = _context.Cards.Find(id);
            if (Card == null) throw new KeyNotFoundException("Card not found");
            return Card;
        }

        public CardResponse GetById(int id)
        {
            var Card = GetCard(id);
            return _mapper.Map<CardResponse>(Card);
        }

        public CardResponse Create(CardCreateRequest model, string userName)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == userName);
            // validate

            // map model to new Card object
            var card = _mapper.Map<Card>(model);
            //card.Account


            // save Card
           // _context.Cards.Add(Card);
            _context.SaveChanges();

            return _mapper.Map<CardResponse>(card);
        }

        public CardResponse Update(int id, CardUpdateRequest model)
        {
            var Card = GetCard(id);

            // validate
            //if (user.UserName != model.Email && _context.Users.Any(x => x.UserName == model.Email))
            //    throw new AppException($"Email '{model.Email}' is already registered");


            // copy model to Card and save
            _mapper.Map(model, Card);
            _context.Cards.Update(Card);
            _context.SaveChanges();

            return _mapper.Map<CardResponse>(Card);
        }

        public void Delete(int id)
        {
            var Card = GetCard(id);
            _context.Cards.Remove(Card);
            _context.SaveChanges();
        }

    }
}
