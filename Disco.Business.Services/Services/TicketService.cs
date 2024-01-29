﻿using Disco.Business.Interfaces.Enums;
using Disco.Business.Interfaces.Interfaces;
using Disco.Domain.Interfaces.Interfaces;
using Disco.Domain.Models.Models;

namespace Disco.Business.Services.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(
            ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task CreateAsync(Ticket ticket)
        {
            ticket.Owner.CreatedTickets.Add(ticket);

            await _ticketRepository.AddAsync(ticket);
        }

        public async Task DeleteAsync(Ticket ticket)
        {
            ticket.Owner.CreatedTickets.Remove(ticket);

            await _ticketRepository.RemoveAsync(ticket);
        }

        public async Task<IEnumerable<TicketSummary>> GetAllAsync(int pageNumber, int pageSize, TicketStatusType statusType)
        {
            switch (statusType)
            {
                case TicketStatusType.Active:
                    return await _ticketRepository.GetAllAsync(pageNumber, pageSize);
                case TicketStatusType.Archived:
                    return await _ticketRepository.GetAllArchivedAsync(pageNumber,pageSize);
                default:
                    return await _ticketRepository.GetAllAsync(pageNumber, pageSize);
            }
        }

        public async Task<TicketSummary> GetAsync(int id)
        {
            var ticket = await _ticketRepository.GetTicketAsync(id);

            return ticket;
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            await _ticketRepository.UpdateAsync(ticket);
        }

        public int Count()
        {
            return _ticketRepository.GetTicketsCount();
        }

        public async Task<Ticket> GetTicketAsync(int id)
        {
            var ticket = await _ticketRepository.GetAsync(id);

            return ticket;
        }
    }
}
