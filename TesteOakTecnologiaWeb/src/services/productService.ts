const API_URL = 'http://localhost:1111/api/Product';

export const getProductsByUserId = async () => {
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

export const createProduct = async (userId: number, name: string, description: string, amount: number, availableSale: boolean) => {
  try {
    const token = localStorage.getItem('token')
    const response = await fetch(API_URL, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
      },
      body: JSON.stringify({ userId, name, description, amount, availableSale}),
    });
    if (!response.ok) {
      throw new Error('Erro ao enviar os dados a API');
    }
    const data = await response.json();
    return data.data;
  } catch (error) {
    console.error('Erro ao enviar os dados a API:', error);
    throw error;
  }
}