import { useState, useEffect } from "react";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import Loading from "../Loading";

import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import Pagination from '@mui/material/Pagination';

const TeamPlayers = (props) => {
    const team = props.teamId;
    const [players, setPlayers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [page, setPageApi] = useState(1);

    const handlePageChange = (event, pageNumber) => {
        setPageApi(pageNumber)
    }

    function getAge(dateString) 
    {
        var today = new Date();
        var birthDate = new Date(dateString);
        var age = today.getFullYear() - birthDate.getFullYear();
        var m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) 
        {
            age--;
        }
        return age;
    }

    useEffect(() => {
        GeneralAxiosService.getMethod('https://localhost:7067/api/v1/Teams/'+ team +'/Players/' + page)
        .then((response) => setPlayers(response.data))
        .then(() => setLoading(false));
    }, [page])

    return <div>
        {loading && <Loading/>}
        {!loading && <TableContainer component={Paper}>
      <Table className="teamPlayersTable" sx={{ minWidth: 500 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Player</TableCell>
            <TableCell align="right">Birth Date (Age)</TableCell>
            <TableCell align="right">Country</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {players.pageResults.sort((a, b) => a.playerPerson.name.localeCompare(b.playerPerson.name)).map((p) => (
            <TableRow
              key={p.id}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                <div className="flex flex-ai-c">
                    <img className="playerImg" src={p.playerPerson.image} alt="person"/>
                    <div>
                        <h4 className="teamPlayersTablePlayerName">{p.playerPerson.name}</h4>
                        <p className="teamPlayersTablePlayerPosition">{p.position}</p>
                    </div>
                </div>
              </TableCell>
              <TableCell align="right">{p.playerPerson.birthDate.substring(0, p.playerPerson.birthDate.indexOf('T'))} ({getAge(p.playerPerson.birthDate)})</TableCell>
              <TableCell align="right">{p.playerPerson.country}</TableCell>
            </TableRow>
          ))}
          <TableRow>
            <TableCell colSpan={4} >
                <Pagination count={players.totalPages} variant="outlined" shape="rounded" onChange={handlePageChange} />
            </TableCell>
          </TableRow>
        </TableBody>
      </Table>
    </TableContainer>}
    </div>
}

export default TeamPlayers;