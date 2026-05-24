import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Header from './Header';
import Home from './pages/Home';
import CategoryList from './pages/Categories/CategoryList';
import CategoryForm from './pages/Categories/CategoryForm';
import BookList from './pages/Books/BookList';
import BookForm from './pages/Books/BookForm';
import MemberList from './pages/Members/MemberList';
import MemberForm from './pages/Members/MemberForm';
import BorrowedList from './pages/BorrowedRecords/BorrowedRecordList';
import BorrowForm from './pages/BorrowedRecords/BorrowForm';
import './index.css';

function App() {
	return (
		<Router>
			<Header />
			<Routes>
				<Route path="/" element={<Home />} />

				<Route path="/books" element={<BookList />} />
				<Route path="/books/create" element={<BookForm />} />
				<Route path="/books/:id/edit" element={<BookForm />} />

				<Route path="/categories" element={<CategoryList />} />
				<Route path="/categories/create" element={<CategoryForm />} />
				<Route path="/categories/:id/edit" element={<CategoryForm />} />

				<Route path="/members" element={<MemberList />} />
				<Route path="/members/create" element={<MemberForm />} />
				<Route path="/members/:id/edit" element={<MemberForm />} />

				<Route path="/borrowed-records" element={<BorrowedList />} />
				<Route path="/borrowed-records/create" element={<BorrowForm />} />
			</Routes>
		</Router>
	);
}

export default App;
