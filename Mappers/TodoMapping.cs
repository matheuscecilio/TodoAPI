using AutoMapper;
using TodoAPI.Dtos;
using TodoAPI.Models;

namespace TodoAPI.Mappers;

public class TodoMapping : Profile
{
    public TodoMapping()
    {
        CreateMap<TodoItem, TodoItemPatchRequest>();
        CreateMap<TodoItemPatchRequest, TodoItem>();
    }
}
