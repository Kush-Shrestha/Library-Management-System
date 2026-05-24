import { apiClient } from './client.js';

export const borrowedRecordsApi = {
  list: () => apiClient.get('/api/BorrowedRecord'),
  getById: (id) => apiClient.get(`/api/BorrowedRecord/${id}`),
  create: (payload) => apiClient.post('/api/BorrowedRecord', payload),
  returnBook: (id, payload) => apiClient.put(`/api/BorrowedRecord/${id}`, payload),
  remove: (id) => apiClient.del(`/api/BorrowedRecord/${id}`),
  unreturned: () => apiClient.get('/api/BorrowedRecord/unreturned'),
  listByMemberId: (memberId) =>
    apiClient.get(`/api/BorrowedRecord/member/${memberId}`),
  listByBookId: (bookId) => apiClient.get(`/api/BorrowedRecord/book/${bookId}`)
};

