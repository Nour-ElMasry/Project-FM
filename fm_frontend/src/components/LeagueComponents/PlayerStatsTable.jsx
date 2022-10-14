import TableContainer from '@mui/material/TableContainer';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import MenuItem from '@mui/material/MenuItem';
import Select from '@mui/material/Select';
import { useNavigate } from 'react-router-dom';

const PlayerStatsTable = (props) => {
    const navigate = useNavigate();
    
    return <TableContainer className="tablesContainer container--styled">
        <Table className="teamPlayersTable" aria-label="simple table">
         <TableHead>
           <TableRow sx={{cursor: "default"}}>
             <TableCell>
                <Select
                    labelId="demo-simple-select-label"
                    id="demo-simple-select"
                    value={props.url}
                    onChange={props.handleChange}
                >
                    <MenuItem value={"Scorers"}>Top Scorers</MenuItem>
                    <MenuItem value={"Assisters"}>Top Assisters</MenuItem>
                    <MenuItem value={"CleanSheets"}>Top Clean sheets</MenuItem>
                </Select>
             </TableCell>
        
             <TableCell align="center"><h4>Team</h4></TableCell>
        
             <TableCell align="center"><h4>Stat</h4></TableCell>
           </TableRow>
         </TableHead>
         <TableBody>
           {props.players.map((p) => (
             <TableRow
               hover
               onClick={() => navigate("/Players/" + p.id)}
               key={p.id}
               sx={{ '&:last-child td, &:last-child th': { border: 0 }, cursor: "pointer"}}>
        
               <TableCell component="th" scope="row">
                 <div className="flex flex-ai-c">
                     <img className="playerImg" src={p.playerPerson?.image} alt="person"/>
                     <div>
                         <h4 className="teamPlayersTablePlayerName">{p.playerPerson?.name}</h4>
                         <p className="teamPlayersTablePlayerPosition">{p.position}</p>
                     </div>
                 </div>
               </TableCell>
        
                <TableCell align="center">
                   <img className="teamLogo" src={p.currentTeam?.teamLogo} alt="person"/>
                </TableCell>
                
                <TableCell align="center">
                    {props.type === "Scorers" && <h4>{p.playerRecord?.goals}</h4>}
                    {props.type === "Assisters"  && <h4>{p.playerRecord?.assists}</h4>}
                    {props.type === "CleanSheets" && <h4>{p.playerRecord?.cleanSheets}</h4>}
                </TableCell>
             </TableRow>
           ))}
         </TableBody>
        </Table>      
    </TableContainer>
}

export default PlayerStatsTable;