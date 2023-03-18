import React, { useEffect, useState } from 'react';

export default function App() {
    const [refresh, setRefresh] = useState(true);
    const [todos, setToDos] = useState([]);
    const [todoToAdd, setToDoToAdd] = useState('');

    useEffect(() => {
        const fetchData = async () => {
            const todos = await getToDoItems();
            setToDos(todos);
        }

        fetchData().catch(console.error);
    }, [refresh]);

    const onRefresh = () => {
        setRefresh(!refresh);
    }

    const onDelete = async (id) => {
        await deleteToDo(id);
        onRefresh();
    }

    const onAdd = async () => {
        await addToDo(todoToAdd);
        setToDoToAdd('');
        onRefresh();
    }

    const onUpdate = async (id, todo) => {
        await updateToDo(id, todo);
        onRefresh();
    }

    return (
        <table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>ToDo List</th>
                </tr>
            </thead>
            <tbody>
                {todos.map(todo =>
                    <ToDoItem todo={todo} onDelete={onDelete} onUpdate={onUpdate} />
                )}
                <td><input value={ todoToAdd } type="text" onChange={(event) => setToDoToAdd(event.target.value)} /></td>
                <td><button onClick={ () => onAdd() }>Add</button></td>
            </tbody>
           
           
        </table>
    );
}

function ToDoItem({ todo, onDelete, onUpdate }) {
    const [name, setName] = useState(todo.ToDoItem);

    useEffect(() => {
        setName(todo.ToDoItem);
    }, [todo.ToDoItem]);

    return (
        <tr key={todo.Id}>
            <td> <input value={name} type="text" onChange={ (event) => setName(event.target.value) } /></td>
            <td><button onClick={() => onDelete(todo.Id)}>Delete</button></td>
            <td><button onClick={ () => onUpdate(todo.Id, name) }>Update</button></td>
        </tr>
    )
}

async function getToDoItems() {
    console.log("fetch started");
    const response = await fetch('http://localhost:62116/ToDo/Get');
    const data = await response.json();
    console.log(data);

    return data;
}

async function deleteToDo(id) {
    console.log("delete started: " + id);

    await fetch('http://localhost:62116/ToDo/Delete?id=' + id, { method: "DELETE" });
}

async function addToDo(todo) {
    console.log("add started: " + todo);

    await fetch('http://localhost:62116/ToDo/Post', {
        method: "POST", headers: {
            "Content-Type": "application/json",
        }, body: JSON.stringify({ 'ToDoItem': todo }) });
}

async function updateToDo(id, todo) {
    console.log("update started: " + id + " - " + todo);

    await fetch('http://localhost:62116/ToDo/Put', {
        method: "PUT", headers: {
            "Content-Type": "application/json",
    }, body: JSON.stringify({ Id: id, 'ToDoItem': todo })
});
}
