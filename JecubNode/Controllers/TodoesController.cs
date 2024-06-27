﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JecubNode;

namespace Jecub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoesController : ControllerBase
    {
        private readonly DataContext _context;

        public TodoesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Todoes
        [HttpGet("/Read")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTask(int id)
        {
            List<Todo> tmp = await _context.Task.ToListAsync();
            return Ok(tmp.Where(x=>x.User_Id == id).ToList());
        }

        // GET: api/Todoes/5
        [HttpGet("/ReadByID")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await _context.Task.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // PUT: api/Todoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/Update")]
        public async Task<IActionResult> PutTodo(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Todoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/Create")]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            _context.Task.Add(todo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Todoes/5
        [HttpDelete("/Delete")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Task.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Task.Remove(todo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TodoExists(int id)
        {
            return _context.Task.Any(e => e.Id == id);
        }
    }
}
