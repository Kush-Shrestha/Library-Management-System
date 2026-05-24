import { apiClient } from './client.js';

export const membersApi = {
  list: () => apiClient.get('/api/Member'),
  listWithBorrowRecords: () => apiClient.get('/api/Member/with-borrow-records'),
  getById: (id) => apiClient.get(`/api/Member/${id}`),
  getByEmail: (email) => apiClient.get(`/api/Member/email/${encodeURIComponent(email)}`),
  create: (payload) => apiClient.post('/api/Member', payload),
  update: (id, payload) => apiClient.put(`/api/Member/${id}`, payload),
  remove: (id) => apiClient.del(`/api/Member/${id}`)
};

