import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import RatingTableCell from '../RatingTableCell';
import CountryTableItem from '../CountryTableItem';
import Pagination from '../Pagination';
import FilterToolBar from '../FilterTooBar';

const PlayersTable = (props) => {
    const players = props.players;
    const isPlayersPage = props.playersPage;

    const getAge = (dateString) => {
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
    
    const dateLongFormat = (date) => {
      var options = { year: 'numeric', month: 'short', day: 'numeric' };
      var today  = new Date(date);

      return today.toLocaleDateString("en-US", options);
    }

    return <div className='container--styledDarker'>
    <FilterToolBar team={true} handleFilterSubmit={props.handleFilterSubmit}/>
    <TableContainer className="tablesContainer">
      <Table className="teamPlayersTable" sx={{ minWidth: 500 }} aria-label="simple table">
        <TableHead>
          <TableRow sx={{cursor: "default"}}>
            <TableCell><h4>Player Info</h4></TableCell>
            
            {isPlayersPage && <>
              <TableCell className='tableColumnHide' align="center"><h4>Team</h4></TableCell>
              <TableCell className='tableColumnHide' align="center"><h4>Attacking</h4></TableCell>
              <TableCell className='tableColumnHide' align="center"><h4>PlayMaking</h4></TableCell>
              <TableCell className='tableColumnHide' align="center"><h4>Defending</h4></TableCell>
              <TableCell className='tableColumnHide' align="center"><h4>GoalKeeping</h4></TableCell>
            </>}

            <TableCell align="center"><h4>Country</h4></TableCell>
            <TableCell align="center"><h4>Birth Date (Age)</h4></TableCell> 

          </TableRow>
        </TableHead>
        <TableBody>
          {players?.pageResults?.sort((a, b) => a.playerPerson.name.localeCompare(b.playerPerson.name)).map((p) => (
            <TableRow
              key={p.id}
              sx={{ '&:last-child td, &:last-child th': { border: 0 }, cursor: "default"}}>

              <TableCell component="th" scope="row">
                <div className="flex flex-ai-c">
                    <img className="playerImg" src={p.playerPerson?.image} alt="person"/>
                    <div>
                        <h4 className="teamPlayersTablePlayerName">{p.playerPerson?.name}</h4>
                        <p className="teamPlayersTablePlayerPosition">{p.position}</p>
                    </div>
                </div>
              </TableCell>

              {isPlayersPage && <>
                <TableCell className='tableColumnHide' align="center">
                <img className="teamLogo" src={p.currentTeam?.teamLogo} alt="person"/></TableCell>
                <RatingTableCell rating={p.playerStats?.attacking}/>
                <RatingTableCell rating={p.playerStats?.playMaking}/>
                <RatingTableCell rating={p.playerStats?.defending}/>
                <RatingTableCell rating={p.playerStats?.goalkeeping}/>
              </>}

              <CountryTableItem country={p.playerPerson?.country}/>
              
              <TableCell align="center">{dateLongFormat(p.playerPerson?.birthDate)} ({getAge(p.playerPerson.birthDate)})</TableCell>
            
            </TableRow>
          ))}
          <TableRow>

            <TableCell colSpan={4} >
              <Pagination page={players?.currentPage} maxPages={players?.totalPages} pageLoading={props.loading} handlePageChange={props.handlePageChange}/>
            </TableCell>

          </TableRow>
        </TableBody>
      </Table>
    </TableContainer>
  </div>
}

export default PlayersTable;