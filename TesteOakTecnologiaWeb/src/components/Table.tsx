import React, { useEffect, useState } from 'react';
import {
  Button,
  Container,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  TextField,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  TableSortLabel,
  MenuItem,
} from '@mui/material';
import { getProductsByUserId, createProduct } from '../services/productService';
import { userData } from '../services/userService';

interface Product {
  userId: number;
  name: string;
  description: string;
  amount: number;
  availableSale: boolean;
}

type Order = 'asc' | 'desc';

const ProductTable: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [userId, setUserId] = useState<number | null>(null);
  const [userName, setUserName] = useState<string>('');


  const [order, setOrder] = useState<Order>('asc');
  const [orderBy, setOrderBy] = useState<keyof Product>('amount');

  const [openDetails, setOpenDetails] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);

  const fetchProducts = async () => {
    try {
      const data = await getProductsByUserId();
      const user = await userData();
      if (user.length > 0) {
        setUserId(user[0].userId);
        setUserName(user[0].name);

      }
      setProducts(data);
    } catch (error) {
      console.error('Erro ao buscar os dados da API:', error);
    }
  };

  useEffect(() => {
    fetchProducts();
  }, []);

  const [open, setOpen] = useState(false);
  const [newProduct, setNewProduct] = useState<Omit<Product, 'userId'>>({
    name: '',
    description: '',
    amount: 0,
    availableSale: true,
  });

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | { name?: string; value: unknown }>) => {
    const { name, value } = e.target as { name: keyof Product; value: string };
    setNewProduct((prev) => ({
      ...prev,
      [name]: name === 'availableSale' ? value === 'true' : value,
    }));
  };

  const handleAddProduct = async () => {
    if (userId !== null) {
      try {
        const createdProduct = await createProduct(
          userId,
          newProduct.name,
          newProduct.description,
          newProduct.amount,
          newProduct.availableSale
        );
        setProducts((prev) => [...prev, createdProduct]);
        setNewProduct({ name: '', description: '', amount: 0, availableSale: true });
        handleClose();
      } catch (error) {
        console.error('Erro ao adicionar o produto:', error);
      }
    } else {
      console.error('User ID não encontrado. Não é possível adicionar o produto.');
    }
  };

  const handleRequestSort = (property: keyof Product) => {
    const isAsc = orderBy === property && order === 'asc';
    setOrder(isAsc ? 'desc' : 'asc');
    setOrderBy(property);
  };

  const sortProducts = (array: Product[], comparator: (a: Product, b: Product) => number) => {
    const stabilizedThis = array.map((el, index) => [el, index] as [Product, number]);
    stabilizedThis.sort((a, b) => {
      const order = comparator(a[0], b[0]);
      if (order !== 0) return order;
      return a[1] - b[1];
    });
    return stabilizedThis.map((el) => el[0]);
  };

  const getComparator = (order: Order, orderBy: keyof Product) => {
    return order === 'desc'
      ? (a: Product, b: Product) => (b[orderBy] < a[orderBy] ? -1 : 1)
      : (a: Product, b: Product) => (a[orderBy] < b[orderBy] ? -1 : 1);
  };

  const handleOpenDetails = (product: Product) => {
    setSelectedProduct(product);
    setOpenDetails(true);
  };

  const handleCloseDetails = () => {
    setOpenDetails(false);
    setSelectedProduct(null);
  };

  return (
    <Container
      maxWidth="sm"
      style={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        height: '100vh',
        justifyContent: 'center',
      }}
    >
      {userName && <h1>Olá, {userName}</h1>}

      <Button variant="contained" color="primary" onClick={handleClickOpen}>
        Add Product
      </Button>
      <TableContainer component={Paper} style={{ maxHeight: 400, marginTop: 20 }}>
        <Table stickyHeader>
          <TableHead>
            <TableRow>
              <TableCell align="center">Nome do Produto</TableCell>
              <TableCell align="center">
                <TableSortLabel
                  active={orderBy === 'amount'}
                  direction={orderBy === 'amount' ? order : 'asc'}
                  onClick={() => handleRequestSort('amount')}
                >
                  Valor do Produto
                </TableSortLabel>
              </TableCell>
              <TableCell align="center">Details</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {sortProducts(products, getComparator(order, orderBy)).map((product, index) => (
              <TableRow key={index}>
                <TableCell align="center">{product.name}</TableCell>
                <TableCell align="center">{product.amount}</TableCell>
                <TableCell align="center">
                  <Button variant="contained" color="primary" onClick={() => handleOpenDetails(product)}>
                    View Details
                  </Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>Adicionar um novo Produto</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            name="name"
            label="Nome do Produto"
            type="text"
            fullWidth
            value={newProduct.name}
            onChange={handleChange}
          />
          <TextField
            margin="dense"
            name="amount"
            label="Valor"
            type="number"
            fullWidth
            value={newProduct.amount}
            onChange={handleChange}
          />
          <TextField
            margin="dense"
            name="description"
            label="Descrição"
            type="text"
            fullWidth
            value={newProduct.description}
            onChange={handleChange}
          />
          <TextField
            select
            margin="dense"
            name="availableSale"
            label="Disponível para venda?"
            fullWidth
            value={newProduct.availableSale.toString()}
            onChange={handleChange}
          >
            <MenuItem value="true">Sim</MenuItem>
            <MenuItem value="false">Não</MenuItem>
          </TextField>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} color="primary">
            Cancelar
          </Button>
          <Button onClick={handleAddProduct} color="primary">
            Adicionar
          </Button>
        </DialogActions>
      </Dialog>

      <Dialog open={openDetails} onClose={handleCloseDetails}>
        <DialogTitle>Detalhes do Produto</DialogTitle>
        <DialogContent>
          {selectedProduct && (
            <>
              <p><strong>Nome:</strong> {selectedProduct.name}</p>
              <p><strong>Descrição:</strong> {selectedProduct.description}</p>
              <p><strong>Valor:</strong> {selectedProduct.amount}</p>
              <p><strong>Disponível para venda:</strong> {selectedProduct.availableSale ? 'Sim' : 'Não'}</p>
            </>
          )}
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseDetails} color="primary">
            Fechar
          </Button>
        </DialogActions>
      </Dialog>

    </Container>
  );
};

export default ProductTable;
