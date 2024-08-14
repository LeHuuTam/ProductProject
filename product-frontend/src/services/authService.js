import axios from 'axios';

const AUTH_URL = 'https://productmanagement20240814103858.azurewebsites.net/auth';

export const login = (user) => axios.post(`${AUTH_URL}/login`, user);
export const logout = () => axios.post(`${AUTH_URL}/logout`);