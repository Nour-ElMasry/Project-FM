import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import RatingTableCell from '../RatingTableCell';
import CountryItem from '../CountryItem';
import Pagination from '../Pagination';
import FilterToolBar from '../FilterTooBar';
import { useNavigate } from 'react-router-dom';
import DateService from '../../services/DateService';

const PlayersTable = (props) => {
    const players = props.players;
    const isPlayersPage = props.playersPage;
    const navigate = useNavigate();

    const getDate = (date) => {
      var dateAge = DateService.getDateAndAge(date);
      return dateAge.date + " (" + dateAge.age + ")";
    }

    return <> 
    { !(players.pageResults.length > 0) && <h2 className='errorMsg'>No Players Found!</h2>}
    <div className='container--styledDarker'>

    <FilterToolBar 
      resetFilterData={props.resetFilterData} 
      team={props.team} 
      handleFilterSubmit={props.handleFilterSubmit}
    />

    <TableContainer className="tablesContainer">
      <Table className="teamPlayersTable" sx={{ minWidth: 500 }} aria-label="simple table">
        <TableHead>
          <TableRow sx={{cursor: "default"}}>
            <TableCell><h4>Player Info</h4></TableCell>

            <TableCell align="center"><h4>Rating</h4></TableCell>

            {isPlayersPage && <>
              <TableCell align="center"><h4>Team</h4></TableCell>
              <TableCell className='tableColumnHide' align="center"><h4>Attacking</h4></TableCell>
              <TableCell className='tableColumnHide' align="center"><h4>PlayMaking</h4></TableCell>
              <TableCell className='tableColumnHide' align="center"><h4>Defending</h4></TableCell>
              <TableCell className='tableColumnHide' align="center"><h4>GoalKeeping</h4></TableCell>
            </>}

            <TableCell align="center"><h4>Country</h4></TableCell>
            <TableCell className="ageColumn" align="center"><h4>Birthday (Age)</h4></TableCell> 

          </TableRow>
        </TableHead>
        <TableBody>
          {players?.pageResults?.sort((a, b) => a.playerPerson.name.localeCompare(b.playerPerson.name)).map((p) => (
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

              <RatingTableCell overall rating={p.playerStats?.overallRating}/>

              {isPlayersPage && <>
                <TableCell align="center">
                  <img className="teamLogo" src={p.currentTeam?.teamLogo} alt="person"/>
                </TableCell>
                <RatingTableCell hide rating={p.playerStats?.attacking}/>
                <RatingTableCell hide rating={p.playerStats?.playMaking}/>
                <RatingTableCell hide rating={p.playerStats?.defending}/>
                <RatingTableCell hide rating={p.playerStats?.goalkeeping}/>
              </>}

              
              <TableCell className="countryTableImg" align="center">
                <CountryItem country={p.playerPerson?.country}/>
              </TableCell>
              
              <TableCell align="center">{getDate(p.playerPerson?.birthDate)}</TableCell>
            
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
  </div></>
}

export default PlayersTable;