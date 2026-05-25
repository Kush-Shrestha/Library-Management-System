import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { categoriesApi } from '../../../lib/api/categories.js';

import '../Pages.css';

export default function CategoryList() {
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(null);

  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await categoriesApi.list();
      setCategories(data);
    } catch (err) {
      setError(err.message || 'Failed to fetch categories');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    if (!window.confirm('Are you sure you want to delete this category?')) return;

    try {
      await categoriesApi.remove(id);
      setSuccess('Category deleted successfully');
      setCategories(categories.filter(category => category.id !== id));
      setTimeout(() => setSuccess(null), 3000);
    } catch (err) {
      setError(err.message || 'Failed to delete category');
    }
  };

  if (loading) {
    return <div className="page-container"><div className="loading">Loading categories...</div></div>;
  }

  return (
    <div className="page-container">
      <div className="list-header">
        <h1>📚 Categories</h1>
        <Link to="/categories/create" className="btn btn-primary">+ Add Category</Link>
      </div>

      {error && <div className="error">{error}</div>}
      {success && <div className="success">{success}</div>}

      {categories.length === 0 ? (
        <div className="loading">No categories found. <Link to="/categories/create">Add one now</Link></div>
      ) : (
        <div className="table-container">
          <table>
            <thead>
              <tr>
                <th>Name</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
  {(categories ?? []).map(category => (
    <tr key={category.id}>
      <td>{category.name}</td>
      <td className="actions">
        <Link to={`/categories/${category.id}/edit`} className="btn btn-primary btn-sm">Edit</Link>
        <button className="btn btn-danger btn-sm" onClick={() => handleDelete(category.id)}>Delete</button>
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