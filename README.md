# Books WebAPI

This project is a simple web api with books in-mem repository


## Routes
### GET
#### `GET / books`
returns all books in repo

#### `GET / books / {id}`
returns book by id

### POST
#### `POST / books`
creates book with structure as below:
```
{
    "author": "Author", //required
    "title": "Title", //required
    "isRead": false //optional
}
```

#### `POST / books / mail`
sends email to recipient provided in body with unread books

### PUT
#### `PUT / books`
updates book with structure as below:
```
{
    "id": 1, //required
    "author": "New Author", //required
    "title": "New Title", //required
    "isRead": false //optional
}
```

#### `DELETE / books / {id}`
deletes book by id

#### `PATCH / books / {id}`
marks book as read by id
