import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import { useEffect, useState } from 'react';
import DateService from '../../services/DateService';
import GeneralAxiosService from '../../services/GeneralAxiosService';
import CountryItem from '../CountryItem';
import ErrorMsg from '../ErrorMsg';
import Loading from '../Loading';
import Pagination from '../Pagination';
import Button from '@mui/material/Button';
import DeleteIcon from '@mui/icons-material/Delete';

const getPageNumber = () => {
    if(sessionStorage && parseInt(sessionStorage.getItem("UsersPage_Key")) > 0) {
      return parseInt(sessionStorage.getItem("UsersPage_Key"));
    }
    return 1;
}

const Users = () => {
    const [user] = useState(JSON.parse(localStorage.getItem("User")));
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [hasError, setHasError] = useState(false);
    const [page, setPageApi] = useState(getPageNumber());
    const [refresh, setRefresh] = useState(0);

    const handlePageChange = (pageNumber) => {
        setPageApi(pageNumber);
        sessionStorage.setItem("UsersPage_Key", pageNumber)
    }

    const getDate = (date) => {
        var dateAge = DateService.getDateAndAge(date);
        return dateAge.date + " (" + dateAge.age + ")";
    }

    const DeleteUser = (userId) => {
        GeneralAxiosService.deleteMethod("https://localhost:7067/api/v1/Users/" + userId)
        .then(() => setRefresh(prev => prev + 1))
    }

    useEffect(() => {
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Users/All/" + page)
        .then((res) => setUsers(res.data))
        .then(() => setLoading(false))
        .catch((err) => {
            setLoading(false)
            setHasError(true)
        })
    }, [page, refresh]);

    return <>
        {loading && <Loading />}
        {hasError && <ErrorMsg />}
        {(!loading && !hasError) && <div className="userSection container container--pa">
            <TableContainer className="tablesContainer container--styled">
              <Table className="UsersTable" sx={{ minWidth: 600 }} aria-label="simple table">
                <TableHead>
                  <TableRow sx={{cursor: "default"}}>
                    <TableCell><h4>Username</h4></TableCell>

                    <TableCell align="center"><h4>Name</h4></TableCell>

                    <TableCell align="center"><h4>Country</h4></TableCell>
                    <TableCell className="ageColumn" align="center"><h4>Birthday (Age)</h4></TableCell> 
                    <TableCell align="center"><h4>Actions</h4></TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                {console.log(users)}
                  {users?.pageResults.filter(u => u.id !== user.customer.userId).map((p) => (
                    <TableRow
                      key={p.id}
                      sx={{ '&:last-child td, &:last-child th': { border: 0 }}}>

                      <TableCell component="th" scope="row">
                        <div className="flex flex-ai-c">
                            <img className="userImg" src={p.userPerson?.image} alt="person"/>
                            <div>
                                <h4>{p.username}</h4>
                            </div>
                        </div>
                      </TableCell>

                      <TableCell align="center"><h4>{p.userPerson?.name}</h4></TableCell>

                      <TableCell className="countryTableImg" align="center">
                        <CountryItem country={p.userPerson?.country}/>
                      </TableCell>

                      <TableCell align="center">{getDate(p.userPerson?.birthDate)}</TableCell>
                      <TableCell align="center">
                        <Button variant="contained" color='error' size='small' disableElevation
                            onClick={() => DeleteUser(p.id)}>
                            <DeleteIcon fontSize='small' /> Delete
                        </Button>
                       </TableCell>
                    </TableRow>
                  ))}
                  <TableRow>

                    <TableCell colSpan={4} >
                      <Pagination page={page} maxPages={users?.totalPages} pageLoading={loading} handlePageChange={handlePageChange}/>
                    </TableCell>

                  </TableRow>
                </TableBody>
              </Table>
            </TableContainer>
        </div>}
    </>
}

export default Users;