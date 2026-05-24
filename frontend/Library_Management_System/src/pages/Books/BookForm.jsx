import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { booksApi } from '../../../lib/api/books.js';
import { categoriesApi } from '../../../lib/api/categories.js';

import '../Pages.css';

export default function BookForm() {
  const navigate = useNavigate();
  const { id } = useParams();
  const isEditing = !!id;

  const [formData, setFormData] = useState({
    title: '',
    author: '',
    isbn: '',
    categoryID: ''
  });
  const [categories, setCategories] = useState([]);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    loadInitialData();
  }, [id]);

  const loadInitialData = async () => {
    try {
      setLoading(true);
      const [categoriesData, book] = await Promise.all([
        categoriesApi.list(),
        isEditing ? booksApi.getById(parseInt(id)) : Promise.resolve(null)
      ]);

      setCategories(categoriesData);

      if (book) {
        setFormData({
          title: book.title,
          author: book.author,
          isbn: book.isbn,
          categoryID: book.categoryID ?? ''
        });
      }
    } catch (err) {
      setError(err.message || 'Failed to load book data');
    } finally {
      setLoading(false);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: name === 'categoryID' ? (value === '' ? '' : parseInt(value, 10)) : value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError(null);
    setLoading(true);

    try {
      if (isEditing) {
        await booksApi.update(parseInt(id), formData);
      } else {
        await booksApi.create(formData);
      }
      navigate('/books');
    } catch (err) {
      setError(err.message || 'Failed to save book');
    } finally {
      setLoading(false);
    }
  };

  if (loading && isEditing) return <div className="page-container"><div className="loading">Loading...</div></div>;

  return (
    <div className="page-container">
      <div className="form-container">
        <h1>{isEditing ? 'Edit Book' : 'Add New Book'}</h1>

        {error && <div className="error">{error}</div>}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="title">Title *</label>
            <input
              id="title"
              type="text"
              name="title"
              value={formData.title}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-group">
            <label htmlFor="author">Author *</label>
            <input
              id="author"
              type="text"
              name="author"
              value={formData.author}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-group">
            <label htmlFor="isbn">ISBN *</label>
            <input
              id="isbn"
              type="text"
              name="isbn"
              value={formData.isbn}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-group">
            <label htmlFor="categoryID">Category *</label>
            <select
              id="categoryID"
              name="categoryID"
              value={formData.categoryID}
              onChange={handleChange}
              required
            >
              <option value="">Choose a category...</option>
              {categories.map(category => (
                <option key={category.id} value={category.id}>
                  {category.name}
                </option>
              ))}
            </select>
          </div>

          <div className="form-actions">
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Saving...' : isEditing ? 'Update Book' : 'Add Book'}
            </button>
            <button type="button" className="btn btn-secondary" onClick={() => navigate('/books')}>
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
