import './Pages.css';

export default function Home() {
  return (
    <div className="page-container">
      <div className="home-content">
        <h1>Welcome to Library Management System</h1>
        <p>Manage your library inventory and borrowing records efficiently.</p>
        
        <div className="features-grid">
          <div className="feature-card">
            <div className="feature-icon">📖</div>
            <h3>Manage Books</h3>
            <p>Add, edit, and delete books from your library collection.</p>
          </div>
          
          <div className="feature-card">
            <div className="feature-icon">👥</div>
            <h3>Manage Members</h3>
            <p>Keep track of library members and their information.</p>
          </div>
          
          <div className="feature-card">
            <div className="feature-icon">📋</div>
            <h3>Track Borrowings</h3>
            <p>Record book borrowings and returns efficiently.</p>
          </div>
        </div>
      </div>
    </div>
  );
}
