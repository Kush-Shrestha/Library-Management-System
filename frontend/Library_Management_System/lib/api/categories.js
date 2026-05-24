import { apiClient } from './client.js';

export const categoriesApi = {
  list: () => apiClient.get('/api/Categories'),
  listWithBooks: () => apiClient.get('/api/Categories/with-books'),
  getById: (id) => apiClient.get(`/api/Categories/${id}`),
  getByName: (name) => apiClient.get(`/api/Categories/name/${encodeURIComponent(name)}`),
  create: (payload) => apiClient.post('/api/Categories', payload),
  update: (id, payload) => apiClient.put(`/api/Categories/${id}`, payload),
  remove: (id) => apiClient.del(`/api/Categories/${id}`)
};

