# API Reference — LibraryCrud

Base URL: `https://localhost:7107/api`

Resources and endpoints (summary taken from backend controllers):

## Book
- `GET /api/Book` — Get all books
- `GET /api/Book/{id}` — Get book by id
- `POST /api/Book` — Create book
  - Body: `{ title, author, isbn }` (CreateBookDto)
- `PUT /api/Book/{id}` — Update book
  - Body: `{ title, author, isbn }` (UpdateBookDto)
- `DELETE /api/Book/{id}` — Delete book
- `GET /api/Book/category/{categoryId}` — Get books by category id
- `GET /api/Book/isbn/{isbn}` — Get book by ISBN
- `GET /api/Book/with-category` — Get books including category data

## Category
- `GET /api/Categories` — Get all categories
- `GET /api/Categories/{id}` — Get category by id
- `POST /api/Categories` — Create category
  - Body: `{ name }` (CreateCategoryDto)
- `PUT /api/Categories/{id}` — Update category
  - Body: `{ name }` (UpdateCategoryDto)
- `DELETE /api/Categories/{id}` — Delete category
- `GET /api/Categories/name/{name}` — Get category by name
- `GET /api/Categories/with-books` — Get categories with their books

## Member
- `GET /api/Member` — Get all members
- `GET /api/Member/{id}` — Get member by id
- `POST /api/Member` — Create member
  - Body: `{ fullName, email, phoneNumber }` (CreateMemberDto)
- `PUT /api/Member/{id}` — Update member
  - Body: `{ fullName, email, phoneNumber }` (UpdateMemberDto)
- `DELETE /api/Member/{id}` — Delete member
- `GET /api/Member/email/{email}` — Get member by email
- `GET /api/Member/with-borrow-records` — Get members including borrow records

## BorrowedRecord
- `GET /api/BorrowedRecord` — Get all borrow records
- `GET /api/BorrowedRecord/{id}` — Get record by id
- `POST /api/BorrowedRecord` — Create borrow record
  - Body: `{ bookID, memberID, borrowDate }` (CreateBorrowedRecordDto)
- `PUT /api/BorrowedRecord/{id}` — Return book (update record)
  - Body: `{ returnDate }` (UpdateBorrowedRecordDto) — backend returns updated record
- `DELETE /api/BorrowedRecord/{id}` — Delete record
- `GET /api/BorrowedRecord/member/{memberId}` — Member borrow history
- `GET /api/BorrowedRecord/book/{bookId}` — Book borrow history
- `GET /api/BorrowedRecord/unreturned` — Get unreturned books

## User
- `GET /api/Users` — Get all users
- `GET /api/Users/{id}` — Get user by id
- `POST /api/Users` — Create user
  - Body: `{ name, email, password }` (CreateUserDto)
- `PUT /api/Users/{id}` — Update user
  - Body: `{ name, email, password }` (UpdateUserDto)
- `DELETE /api/Users/{id}` — Delete user
- `GET /api/Users/email/{email}` — Get user by email

---

Example curl (get all books):

```bash
curl -k https://localhost:7107/api/Book
```

Use `-k` if backend is using a self-signed certificate.
