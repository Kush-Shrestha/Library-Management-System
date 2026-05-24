import { describe, it, expect, vi } from 'vitest';
import { apiClient } from '../..//lib/api/client.js';

describe('api client smoke', () => {
  it('builds request and handles non-ok responses', async () => {
    const fetchMock = vi.fn(async () => ({
      ok: false,
      status: 500,
      headers: { get: () => 'application/json' },
      json: async () => ({ message: 'boom' })
    }));

    global.fetch = fetchMock;

    await expect(apiClient.get('/test')).rejects.toMatchObject({ status: 500 });
    expect(fetchMock).toHaveBeenCalled();
  });
});

