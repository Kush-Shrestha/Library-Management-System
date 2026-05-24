import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { borrowedRecordsApi } from '../../../lib/api/borrowedRecords.js';
import { booksApi } from '../../../lib/api/books.js';
import { membersApi } from '../../../lib/api/members.js';

import '../Pages.css';

export default function BorrowForm() {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    bookId: '',
    memberId: '',
    borrowDate: new Date().toISOString().split('T')[0]
  });
  const [books, setBooks] = useState([]);
  const [members, setMembers] = useState([]);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      setLoading(true);
      const [booksData, membersData] = await Promise.all([
        booksApi.list(),
        membersApi.list()
      ]);
      setBooks(booksData);
      setMembers(membersData);
    } catch (err) {
      setError(err.message || 'Failed to load data');
    } finally {
      setLoading(false);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: name === 'bookId' || name === 'memberId' ? parseInt(value) : value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError(null);
    setLoading(true);

    try {
      await borrowedRecordsApi.create(formData);
      navigate('/borrowed-records');
    } catch (err) {
      setError(err.message || 'Failed to create borrow record');
    } finally {
      setLoading(false);
    }
  };

  if (loading && books.length === 0) return <div className="page-container"><div className="loading">Loading...</div></div>;

  return (
    <div className="page-container">
      <div className="form-container">
        <h1>Record New Book Borrow</h1>

        {error && <div className="error">{error}</div>}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="bookId">Select Book *</label>
            <select
              id="bookId"
              name="bookId"
              value={formData.bookId}
              onChange={handleChange}
              required
            >
              <option value="">Choose a book...</option>
              {books.map(book => (
                <option key={book.id} value={book.id}>
                  {book.title} - {book.author}
                </option>
              ))}
            </select>
          </div>

          <div className="form-group">
            <label htmlFor="memberId">Select Member *</label>
            <select
              id="memberId"
              name="memberId"
              value={formData.memberId}
              onChange={handleChange}
              required
            >
              <option value="">Choose a member...</option>
              {members.map(member => (
                <option key={member.id} value={member.id}>
                  {member.fullName} - {member.email}
                </option>
              ))}
            </select>
          </div>

          <div className="form-group">
            <label htmlFor="borrowDate">Borrow Date *</label>
            <input
              id="borrowDate"
              type="date"
              name="borrowDate"
              value={formData.borrowDate}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-actions">
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Creating...' : 'Record Borrow'}
            </button>
            <button type="button" className="btn btn-secondary" onClick={() => navigate('/borrowed-records')}>
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
