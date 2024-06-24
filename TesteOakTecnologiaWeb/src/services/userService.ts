const API_URL = 'http://localhost:1111/api/User';

export const login = async (email: string, password: string) => {
  try {
    const response = await fetch(`${API_URL}/Login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ email, password })
    });

    const data = await response.json();
    const token = data.data.token;
    localStorage.setItem("token", token);
    return data;

  } catch (error) {
    console.error('Erro ao realizar login:', error);
    throw error;
  }
}

export const register = async (name: string, email: string, password: string, confirmPassword: string) => {
  try {
    const response = await fetch(`${API_URL}/Register`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ name, email, password, confirmPassword }),
    });

    if (!response.ok) {
      throw new Error('Erro ao realizar o registro');
    }

    const data = await response.json();
    return data;
  } catch (error) {
    console.error('Erro ao realizar o registro:', error);
    throw error;
  }
};

export const userData = async () => {
  try {
    const token = localStorage.getItem('token')
    const response = await fetch(API_URL, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
      }
    });
    if (!response.ok) {
      throw new Error('Erro ao buscar os dados da API');
    }
    const data = await response.json();
    return data.data;
  } catch (error) {
    console.error('Erro ao buscar os dados da API:', error);
    throw error;
  }
}
