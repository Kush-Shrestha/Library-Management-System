import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { membersApi } from '../../../lib/api/members.js';

import '../Pages.css';

export default function MemberList() {
  const [members, setMembers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(null);

  useEffect(() => {
    fetchMembers();
  }, []);

  const fetchMembers = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await membersApi.list();
      setMembers(data);
    } catch (err) {
      setError(err.message || 'Failed to fetch members');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    if (!window.confirm('Are you sure you want to delete this member?')) return;

    try {
      await membersApi.remove(id);
      setSuccess('Member deleted successfully');
      setMembers(members.filter(m => m.id !== id));
      setTimeout(() => setSuccess(null), 3000);
    } catch (err) {
      setError(err.message || 'Failed to delete member');
    }
  };

  if (loading) return <div className="page-container"><div className="loading">Loading members...</div></div>;

  return (
    <div className="page-container">
      <div className="list-header">
        <h1>👥 Members</h1>
        <Link to="/members/create" className="btn btn-primary">+ Add Member</Link>
      </div>

      {error && <div className="error">{error}</div>}
      {success && <div className="success">{success}</div>}

      {members.length === 0 ? (
        <div className="loading">No members found. <Link to="/members/create">Add one now</Link></div>
      ) : (
        <div className="table-container">
          <table>
            <thead>
              <tr>
                <th>Full Name</th>
                <th>Email</th>
                <th>Phone Number</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {members.map(member => (
                <tr key={member.id}>
                  <td>{member.fullName}</td>
                  <td>{member.email}</td>
                  <td>{member.phoneNumber}</td>
                  <td className="actions">
                    <Link to={`/members/${member.id}/edit`} className="btn btn-primary btn-sm">Edit</Link>
                    <button className="btn btn-danger btn-sm" onClick={() => handleDelete(member.id)}>Delete</button>
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
