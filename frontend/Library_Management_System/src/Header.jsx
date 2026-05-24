import { Link } from 'react-router-dom';
import './Header.css';

export default function Header() {
  return (
    <header className="header">
      <div className="header-container">
        <Link to="/" className="logo">
          📚 Library Management
        </Link>
        <nav className="nav">
          <Link to="/" className="nav-link">Home</Link>
          <Link to="/books" className="nav-link">Books</Link>
          <Link to="/categories" className="nav-link">Categories</Link>
          <Link to="/members" className="nav-link">Members</Link>
          <Link to="/borrowed-records" className="nav-link">Borrowed Records</Link>
        </nav>
      </div>
    </header>
  );
}
