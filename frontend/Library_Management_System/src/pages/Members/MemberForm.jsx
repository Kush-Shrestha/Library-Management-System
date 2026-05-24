import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { membersApi } from '../../../lib/api/members.js';

import '../Pages.css';

export default function MemberForm() {
  const navigate = useNavigate();
  const { id } = useParams();
  const isEditing = !!id;

  const [formData, setFormData] = useState({
    fullName: '',
    email: '',
    phoneNumber: ''
  });
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (isEditing) {
      loadMember();
    }
  }, [id]);

  const loadMember = async () => {
    try {
      setLoading(true);
      const member = await membersApi.getById(parseInt(id));
      setFormData({
        fullName: member.fullName,
        email: member.email,
        phoneNumber: member.phoneNumber
      });
    } catch (err) {
      setError(err.message || 'Failed to load member');
    } finally {
      setLoading(false);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError(null);
    setLoading(true);

    try {
      if (isEditing) {
        await membersApi.update(parseInt(id), formData);
      } else {
        await membersApi.create(formData);
      }
      navigate('/members');
    } catch (err) {
      setError(err.message || 'Failed to save member');
    } finally {
      setLoading(false);
    }
  };

  if (loading && isEditing) return <div className="page-container"><div className="loading">Loading...</div></div>;

  return (
    <div className="page-container">
      <div className="form-container">
        <h1>{isEditing ? 'Edit Member' : 'Add New Member'}</h1>

        {error && <div className="error">{error}</div>}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="fullName">Full Name *</label>
            <input
              id="fullName"
              type="text"
              name="fullName"
              value={formData.fullName}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-group">
            <label htmlFor="email">Email *</label>
            <input
              id="email"
              type="email"
              name="email"
              value={formData.email}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-group">
            <label htmlFor="phoneNumber">Phone Number *</label>
            <input
              id="phoneNumber"
              type="tel"
              name="phoneNumber"
              value={formData.phoneNumber}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-actions">
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Saving...' : isEditing ? 'Update Member' : 'Add Member'}
            </button>
            <button type="button" className="btn btn-secondary" onClick={() => navigate('/members')}>
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
