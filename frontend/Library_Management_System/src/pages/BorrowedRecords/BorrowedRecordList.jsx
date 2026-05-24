import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { borrowedRecordsApi } from '../../../lib/api/borrowedRecords.js';

import '../Pages.css';

export default function BorrowedRecordList() {
  const [records, setRecords] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(null);

  useEffect(() => {
    fetchRecords();
  }, []);

  const fetchRecords = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await borrowedRecordsApi.list();
      setRecords(data);
    } catch (err) {
      setError(err.message || 'Failed to fetch borrowed records');
    } finally {
      setLoading(false);
    }
  };

  const handleReturn = async (id) => {
    if (!window.confirm('Mark this book as returned?')) return;

    try {
      await borrowedRecordsApi.returnBook(id, { returnDate: new Date().toISOString() });
      setSuccess('Book returned successfully');
      fetchRecords();
      setTimeout(() => setSuccess(null), 3000);
    } catch (err) {
      setError(err.message || 'Failed to return book');
    }
  };

  const handleDelete = async (id) => {
    if (!window.confirm('Are you sure you want to delete this record?')) return;

    try {
      await borrowedRecordsApi.remove(id);
      setSuccess('Record deleted successfully');
      setRecords(records.filter(r => r.id !== id));
      setTimeout(() => setSuccess(null), 3000);
    } catch (err) {
      setError(err.message || 'Failed to delete record');
    }
  };

  const formatDate = (date) => {
    if (!date) return 'N/A';
    return new Date(date).toLocaleDateString();
  };

  if (loading) return <div className="page-container"><div className="loading">Loading records...</div></div>;

  return (
    <div className="page-container">
      <div className="list-header">
        <h1>📋 Borrowed Records</h1>
        <Link to="/borrowed-records/create" className="btn btn-primary">+ New Borrow</Link>
      </div>

      {error && <div className="error">{error}</div>}
      {success && <div className="success">{success}</div>}

      {records.length === 0 ? (
        <div className="loading">No borrowed records found. <Link to="/borrowed-records/create">Create one now</Link></div>
      ) : (
        <div className="table-container">
          <table>
            <thead>
              <tr>
                <th>Book ID</th>
                <th>Member ID</th>
                <th>Borrow Date</th>
                <th>Return Date</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {records.map(record => (
                <tr key={record.id}>
                  <td>{record.bookId}</td>
                  <td>{record.memberId}</td>
                  <td>{formatDate(record.borrowDate)}</td>
                  <td>{formatDate(record.returnDate)}</td>
                  <td className="actions">
                    {!record.returnDate && (
                      <button className="btn btn-success btn-sm" onClick={() => handleReturn(record.id)}>
                        Return
                      </button>
                    )}
                    <button className="btn btn-danger btn-sm" onClick={() => handleDelete(record.id)}>
                      Delete
                    </button>
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
