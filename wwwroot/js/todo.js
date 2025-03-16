document.addEventListener('DOMContentLoaded', () => {
    loadTodos();
    
    document.getElementById('addTodoForm').addEventListener('submit', async (e) => {
        e.preventDefault();
        const titleInput = document.getElementById('todoTitle');
        const title = titleInput.value.trim();
        
        if (title) {
            await createTodo({ title, isCompleted: false });
            titleInput.value = '';
            await loadTodos();
        }
    });
});

async function loadTodos() {
    const response = await fetch('/api/todo');
    const todos = await response.json();
    const todoList = document.getElementById('todoList');
    todoList.innerHTML = '';
    
    todos.forEach(todo => {
        const item = createTodoElement(todo);
        todoList.appendChild(item);
    });
}

function createTodoElement(todo) {
    const div = document.createElement('div');
    div.className = 'list-group-item d-flex justify-content-between align-items-center';
    
    const checkbox = document.createElement('input');
    checkbox.type = 'checkbox';
    checkbox.checked = todo.isCompleted;
    checkbox.className = 'form-check-input me-2';
    checkbox.addEventListener('change', () => updateTodo(todo.id, checkbox.checked));
    
    const title = document.createElement('span');
    title.textContent = todo.title;
    if (todo.isCompleted) {
        title.style.textDecoration = 'line-through';
    }
    
    const deleteBtn = document.createElement('button');
    deleteBtn.className = 'btn btn-danger btn-sm';
    deleteBtn.textContent = 'Delete';
    deleteBtn.addEventListener('click', () => deleteTodo(todo.id));
    
    div.appendChild(checkbox);
    div.appendChild(title);
    div.appendChild(deleteBtn);
    
    return div;
}

async function createTodo(todo) {
    await fetch('/api/todo', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(todo)
    });
}

async function updateTodo(id, isCompleted) {
    const response = await fetch(`/api/todo/${id}`);
    const todo = await response.json();
    todo.isCompleted = isCompleted;
    
    await fetch(`/api/todo/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(todo)
    });
    
    await loadTodos();
}

async function deleteTodo(id) {
    await fetch(`/api/todo/${id}`, {
        method: 'DELETE'
    });
    await loadTodos();
}
