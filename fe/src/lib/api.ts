// API service for backend communication
const API_BASE_URL = 'http://localhost:5037';

export interface ApiResponse<T> {
  success: boolean;
  data?: T;
  message?: string;
}

export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  roleId: string;
  roleName: string;
}

export interface AuthResponse {
  token: string;
  user: User;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

export interface Alumni {
  id: string;
  userId: string;
  user: User;
  graduationYear: number;
  degree: string;
  major: string;
  currentCompany?: string;
  currentPosition?: string;
  location?: string;
  bio?: string;
  linkedinUrl?: string;
  githubUrl?: string;
  websiteUrl?: string;
  profileImageUrl?: string;
  isPublic: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface CreateAlumniRequest {
  graduationYear: number;
  degree: string;
  major: string;
  currentCompany?: string;
  currentPosition?: string;
  location?: string;
  bio?: string;
  linkedinUrl?: string;
  githubUrl?: string;
  websiteUrl?: string;
  isPublic: boolean;
}

export interface Event {
  id: string;
  title: string;
  description: string;
  location?: string;
  start_date: string;
  end_date: string;
  max_attendees?: number;
  current_attendees: number;
  is_online: boolean;
  meeting_url?: string;
  created_by: string;
  created_at: string;
  updated_at: string;
}

export interface CreateEventRequest {
  title: string;
  description: string;
  location?: string;
  start_date: string;
  end_date: string;
  max_attendees?: number;
  is_online: boolean;
  meeting_url?: string;
}

class ApiService {
  private baseUrl: string;

  constructor(baseUrl: string = API_BASE_URL) {
    this.baseUrl = baseUrl;
  }

  private getAuthHeaders(): HeadersInit {
    const token = localStorage.getItem('authToken');
    return {
      'Content-Type': 'application/json',
      ...(token && { Authorization: `Bearer ${token}` }),
    };
  }

  private async handleResponse<T>(response: Response): Promise<ApiResponse<T>> {
    const data = await response.json();
    
    if (!response.ok) {
      throw new Error(data.message || `HTTP error! status: ${response.status}`);
    }
    
    return data;
  }

  private async request<T>(
    endpoint: string,
    options: RequestInit = {}
  ): Promise<ApiResponse<T>> {
    const url = `${this.baseUrl}${endpoint}`;
    const config: RequestInit = {
      headers: this.getAuthHeaders(),
      ...options,
    };

    try {
      const response = await fetch(url, config);
      return await this.handleResponse<T>(response);
    } catch (error) {
      console.error('API request failed:', error);
      throw error;
    }
  }

  // Authentication endpoints
  async login(credentials: LoginRequest): Promise<ApiResponse<AuthResponse>> {
    return this.request<AuthResponse>('/api/auth/login', {
      method: 'POST',
      body: JSON.stringify(credentials),
    });
  }

  async register(userData: RegisterRequest): Promise<ApiResponse<AuthResponse>> {
    return this.request<AuthResponse>('/api/auth/register', {
      method: 'POST',
      body: JSON.stringify(userData),
    });
  }

  async getCurrentUser(): Promise<ApiResponse<User>> {
    return this.request<User>('/api/auth/me');
  }

  // Alumni endpoints
  async getAlumni(): Promise<ApiResponse<Alumni[]>> {
    return this.request<Alumni[]>('/api/alumni');
  }

  async getAlumniById(id: string): Promise<ApiResponse<Alumni>> {
    return this.request<Alumni>(`/api/alumni/${id}`);
  }

  async createAlumni(alumniData: CreateAlumniRequest): Promise<ApiResponse<Alumni>> {
    return this.request<Alumni>('/api/alumni', {
      method: 'POST',
      body: JSON.stringify(alumniData),
    });
  }

  async updateAlumni(id: string, alumniData: CreateAlumniRequest): Promise<ApiResponse<Alumni>> {
    return this.request<Alumni>(`/api/alumni/${id}`, {
      method: 'PUT',
      body: JSON.stringify(alumniData),
    });
  }

  async deleteAlumni(id: string): Promise<ApiResponse<string>> {
    return this.request<string>(`/api/alumni/${id}`, {
      method: 'DELETE',
    });
  }

  // Events endpoints
  async getEvents(): Promise<ApiResponse<Event[]>> {
    return this.request<Event[]>('/api/events');
  }

  async getEventById(id: string): Promise<ApiResponse<Event>> {
    return this.request<Event>(`/api/events/${id}`);
  }

  async createEvent(eventData: CreateEventRequest): Promise<ApiResponse<Event>> {
    return this.request<Event>('/api/events', {
      method: 'POST',
      body: JSON.stringify(eventData),
    });
  }

  async updateEvent(id: string, eventData: CreateEventRequest): Promise<ApiResponse<Event>> {
    return this.request<Event>(`/api/events/${id}`, {
      method: 'PUT',
      body: JSON.stringify(eventData),
    });
  }

  async deleteEvent(id: string): Promise<ApiResponse<string>> {
    return this.request<string>(`/api/events/${id}`, {
      method: 'DELETE',
    });
  }

  // Health check
  async healthCheck(): Promise<ApiResponse<{ status: string; timestamp: string; service: string }>> {
    return this.request<{ status: string; timestamp: string; service: string }>('/health');
  }

  // Utility methods
  isAuthenticated(): boolean {
    return !!localStorage.getItem('authToken');
  }

  logout(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('user');
  }

  getCurrentUserFromStorage(): User | null {
    const userStr = localStorage.getItem('user');
    return userStr ? JSON.parse(userStr) : null;
  }
}

// Export singleton instance
export const apiService = new ApiService();
