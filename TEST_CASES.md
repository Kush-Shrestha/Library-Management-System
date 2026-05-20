## Library Management System - API Test Cases

### Database Setup
Before testing, ensure the database is created and migrations are applied:
```
dotnet ef database update
```

---

## TEST SUITE 1: Categories API

### 1.1 Create Category
**POST** `/api/categories`
**Body:**
```json
{
  "name": "Fiction"
}
```
**Expected:** 201 Created

### 1.2 Get All Categories
**GET** `/api/categories`
**Expected:** 200 OK, returns list of categories

### 1.3 Get Category By ID
**GET** `/api/categories/1`
**Expected:** 200 OK

### 1.4 Get Category By Name
**GET** `/api/categories/name/Fiction`
**Expected:** 200 OK

### 1.5 Update Category
**PUT** `/api/categories/1`
**Body:**
```json
{
  "name": "Fiction Books"
}
```
**Expected:** 200 OK

### 1.6 Delete Category
**DELETE** `/api/categories/1`
**Expected:** 200 OK

---

## TEST SUITE 2: Books API

### 2.1 Create Book
**POST** `/api/books`
**Body:**
```json
{
  "title": "The Great Gatsby",
  "author": "F. Scott Fitzgerald",
  "isbn": "978-0-7432-7356-5",
  "categoryID": 1
}
```
**Expected:** 201 Created

### 2.2 Get All Books
**GET** `/api/books`
**Expected:** 200 OK, returns list of books with categories

### 2.3 Get Book By ID
**GET** `/api/books/1`
**Expected:** 200 OK, includes category info

### 2.4 Get Book By ISBN
**GET** `/api/books/isbn/978-0-7432-7356-5`
**Expected:** 200 OK

### 2.5 Get Books By Category
**GET** `/api/books/category/1`
**Expected:** 200 OK

### 2.6 Update Book
**PUT** `/api/books/1`
**Body:**
```json
{
  "title": "The Great Gatsby - Revised",
  "author": "F. Scott Fitzgerald",
  "isbn": "978-0-7432-7356-5",
  "categoryID": 1
}
```
**Expected:** 200 OK

### 2.7 Delete Book
**DELETE** `/api/books/1`
**Expected:** 200 OK

---

## TEST SUITE 3: Members API

### 3.1 Create Member
**POST** `/api/members`
**Body:**
```json
{
  "fullName": "John Doe",
  "email": "john@example.com",
  "phoneNumber": "+1234567890"
}
```
**Expected:** 201 Created

### 3.2 Get All Members
**GET** `/api/members`
**Expected:** 200 OK

### 3.3 Get Member By ID
**GET** `/api/members/1`
**Expected:** 200 OK

### 3.4 Get Member By Email
**GET** `/api/members/email/john@example.com`
**Expected:** 200 OK

### 3.5 Update Member
**PUT** `/api/members/1`
**Body:**
```json
{
  "fullName": "John Doe Smith",
  "email": "john.smith@example.com",
  "phoneNumber": "+9876543210"
}
```
**Expected:** 200 OK

### 3.6 Delete Member
**DELETE** `/api/members/1`
**Expected:** 200 OK

---

## TEST SUITE 4: Users (Librarians) API

### 4.1 Create User
**POST** `/api/users`
**Body:**
```json
{
  "name": "Admin User",
  "email": "admin@library.com",
  "password": "SecurePassword123"
}
```
**Expected:** 201 Created

### 4.2 Get All Users
**GET** `/api/users`
**Expected:** 200 OK

### 4.3 Get User By ID
**GET** `/api/users/1`
**Expected:** 200 OK

### 4.4 Get User By Email
**GET** `/api/users/email/admin@library.com`
**Expected:** 200 OK

### 4.5 Update User
**PUT** `/api/users/1`
**Body:**
```json
{
  "name": "Super Admin",
  "email": "superadmin@library.com",
  "password": "SuperSecure456"
}
```
**Expected:** 200 OK

### 4.6 Delete User
**DELETE** `/api/users/1`
**Expected:** 200 OK

---

## TEST SUITE 5: Borrowed Records API

### 5.1 Create Borrow Record
**POST** `/api/borrowedrecords`
**Prerequisites:** Book ID=1, Member ID=1
**Body:**
```json
{
  "bookID": 1,
  "memberID": 1,
  "borrowDate": "2024-01-15T00:00:00"
}
```
**Expected:** 201 Created

### 5.2 Get All Records
**GET** `/api/borrowedrecords`
**Expected:** 200 OK

### 5.3 Get Record By ID
**GET** `/api/borrowedrecords/1`
**Expected:** 200 OK

### 5.4 Get Member Borrow History
**GET** `/api/borrowedrecords/member/1`
**Expected:** 200 OK

### 5.5 Get Book Borrow History
**GET** `/api/borrowedrecords/book/1`
**Expected:** 200 OK

### 5.6 Get Unreturned Books
**GET** `/api/borrowedrecords/unreturned`
**Expected:** 200 OK

### 5.7 Return Book
**PUT** `/api/borrowedrecords/1/return`
**Expected:** 200 OK, returns updated record with current date

### 5.8 Delete Record
**DELETE** `/api/borrowedrecords/1`
**Expected:** 200 OK

---

## ERROR TEST CASES

### E1: Invalid ID (should return 400 or 404)
**GET** `/api/books/0`
**Expected:** 400 Bad Request

### E2: Non-existent Record (should return 404)
**GET** `/api/books/9999`
**Expected:** 404 Not Found

### E3: Duplicate Email (Member)
Create member with email twice
**Expected:** Second request returns error about duplicate email

### E4: Duplicate ISBN (Book)
Create book with ISBN twice
**Expected:** Second request returns error about duplicate ISBN

### E5: Invalid Input (Null Fields)
**POST** `/api/books`
**Body:**
```json
{
  "title": null,
  "author": "Test",
  "isbn": "123",
  "categoryID": 1
}
```
**Expected:** 400 Bad Request

---

## INTEGRATION TEST FLOW

1. Create Category "Science Fiction"
2. Create Book in that category
3. Create Member
4. Create Borrow Record (Member borrows Book)
5. Get Member with Borrow History
6. Return Book
7. Verify ReturnDate is set
8. Get Unreturned Books (should not include returned book)

---

## Notes

- All datetime values should be in ISO 8601 format
- Authentication/Authorization not yet implemented
- CORS is enabled for all origins in Development
- All responses include success flag and consistent formatting
- Error messages are descriptive
