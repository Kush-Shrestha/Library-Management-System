import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { booksApi } from '../../../lib/api/books.js';
import { categoriesApi } from '../../../lib/api/categories.js';


import '../Pages.css';

export default function BookList() {
  const [books, setBooks] = useState([]);
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(null);

  useEffect(() => {
    fetchBooks();
  }, []);

  const fetchBooks = async () => {
    try {
      setLoading(true);
      setError(null);
      const [booksData, categoriesData] = await Promise.all([
        booksApi.list(),
        categoriesApi.list()
      ]);
      setBooks(booksData);
      setCategories(categoriesData);
    } catch (err) {
      setError(err.message || 'Failed to fetch books');
    } finally {
      setLoading(false);
    }
  };

  const getCategoryName = (categoryID) => {
    const category = categories.find(item => item.id === categoryID);
    return category ? category.name : `Category #${categoryID || 'N/A'}`;
  };

  const handleDelete = async (id) => {
    if (!window.confirm('Are you sure you want to delete this book?')) return;

    try {
      await booksApi.remove(id);
      setSuccess('Book deleted successfully');
      setBooks(books.filter(b => b.id !== id));
      setTimeout(() => setSuccess(null), 3000);
    } catch (err) {
      setError(err.message || 'Failed to delete book');
    }
  };

  if (loading) return <div className="page-container"><div className="loading">Loading books...</div></div>;

  return (
    <div className="page-container">
      <div className="list-header">
        <h1>Books</h1>
        <Link to="/books/create" className="btn btn-primary">+ Add Book</Link>
      </div>

      {error && <div className="error">{error}</div>}
      {success && <div className="success">{success}</div>}

      {books.length === 0 ? (
        <div className="loading">No books found. <Link to="/books/create">Add one now</Link></div>
      ) : (
        <div className="table-container">
          <table>
            <thead>
              <tr>
                <th>Title</th>
                <th>Author</th>
                <th>ISBN</th>
                <th>Category</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {books.map(book => (
                <tr key={book.id}>
                  <td>{book.title}</td>
                  <td>{book.author}</td>
                  <td>{book.isbn}</td>
                  <td>{getCategoryName(book.categoryID)}</td>
                  <td className="actions">
                    <Link to={`/books/${book.id}/edit`} className="btn btn-primary btn-sm">Edit</Link>
                    <button className="btn btn-danger btn-sm" onClick={() => handleDelete(book.id)}>Delete</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
