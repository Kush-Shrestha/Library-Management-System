import { apiClient } from './client.js';

export const booksApi = {
  list: () => apiClient.get('/api/Book'),
  listWithCategory: () => apiClient.get('/api/Book/with-category'),
  getById: (id) => apiClient.get(`/api/Book/${id}`),
  getByCategoryId: (categoryId) => apiClient.get(`/api/Book/category/${categoryId}`),
  getByIsbn: (isbn) => apiClient.get(`/api/Book/isbn/${encodeURIComponent(isbn)}`),
  create: (payload) => apiClient.post('/api/Book', payload),
  update: (id, payload) => apiClient.put(`/api/Book/${id}`, payload),
  remove: (id) => apiClient.del(`/api/Book/${id}`)
};

