import { Outlet, Link } from 'react-router-dom';
import { Button, Box } from '@mui/material';

function Auth() {
  return (
    <div>
      <Outlet />
      <Box mt={2}>
        <Button component={Link} to="/login" variant="contained" color="primary" style={{ marginRight: '10px' }}>
          Login
        </Button>
        <Button component={Link} to="/register" variant="contained" color="secondary">
          Registrar
        </Button>
      </Box>
    </div>
  );
}

export default Auth;
