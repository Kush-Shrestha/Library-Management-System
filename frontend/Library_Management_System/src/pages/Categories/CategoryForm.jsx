import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { categoriesApi } from '../../../lib/api/categories.js';

import '../Pages.css';

export default function CategoryForm() {
  const navigate = useNavigate();
  const { id } = useParams();
  const isEditing = !!id;

  const [formData, setFormData] = useState({ name: '' });
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (isEditing) {
      loadCategory();
    }
  }, [id]);

  const loadCategory = async () => {
    try {
      setLoading(true);
      const category = await categoriesApi.getById(parseInt(id));
      setFormData({ name: category.name });
    } catch (err) {
      setError(err.message || 'Failed to load category');
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
        await categoriesApi.update(parseInt(id), formData);
      } else {
        await categoriesApi.create(formData);
      }
      navigate('/categories');
    } catch (err) {
      setError(err.message || 'Failed to save category');
    } finally {
      setLoading(false);
    }
  };

  if (loading && isEditing) {
    return <div className="page-container"><div className="loading">Loading...</div></div>;
  }

  return (
    <div className="page-container">
      <div className="form-container">
        <h1>{isEditing ? 'Edit Category' : 'Add New Category'}</h1>

        {error && <div className="error">{error}</div>}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="name">Name *</label>
            <input
              id="name"
              type="text"
              name="name"
              value={formData.name}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-actions">
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Saving...' : isEditing ? 'Update Category' : 'Add Category'}
            </button>
            <button type="button" className="btn btn-secondary" onClick={() => navigate('/categories')}>
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}