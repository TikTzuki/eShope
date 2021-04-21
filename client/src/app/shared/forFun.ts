interface S {
  g: number;
  h: number;
  f: number;
  matrix;
  identity: number;
  oldIndetity: number;
}

interface Step {
  initString: string;
  s: S[];
  finishString: string;
  OPEN: string;
  CLOSED: string;
}
class stupid{
  OPEN: S[] = [];
  CLOSE: S[] = [];
  identity =0;
  goal = [
    [1, 2, 3],
    [8, 0, 4],
    [7, 6, 5]
  ]
  steps: Step[] = [];
  constructor(
  ) { }

  ngOnInit() {
    let initS: S = {
      matrix: [
        [8, 3, 4],
        [0, 2, 1],
        [7, 6, 5]
      ],
      g: 0,
      h: 0,
      f: 0,
      identity: 0,
      oldIndetity: 0
    }
    let Ss =  this.movation(initS, initS);
    this.OPEN.push(...Ss);
    // console.log('first', this.OPEN);
    this.OPEN.sort(this.compareF);
    this.CLOSE.push(initS);
    let stepForSave: Step = {
      s: [...Ss],
      OPEN: `OPEN = { ${this.OPEN.map(item => { return `S${item.identity}, ` })} }`,
      CLOSED: `CLOSE = { ${this.CLOSE.map(item => { return `S${item.identity}` })} }`,
      finishString: `ThÃªm ${Ss.map(item => { return `S${item.identity}, ` })} vÃ o táº­p OPEN, thÃªm S${initS.identity} vÃ o CLOSED `,
      initString: `Láº¥y S${initS.identity} ra khá»�i OPEN`
    }
    this.steps.push(stepForSave)
    let i = 30;
    // console.log('first', this.OPEN);
    while (!this.checkSuccess(this.goal) && this.OPEN.length !== 0 && i > 0) {
      // console.log(i, this.OPEN);
      let s = this.OPEN.shift();
      // console.log(s);
      let sOlder = this.CLOSE[this.CLOSE.length - 1];
      let smallest = this.movation(s, sOlder);
      this.OPEN.push(...smallest);
      this.CLOSE.push(s);
      this.OPEN.sort(this.compareF);
      let stepForSave:Step = {
        s: [...smallest],
        OPEN: `OPEN = { ${this.OPEN.map(item =>{ return `S${item.identity} `})} }`,
        CLOSED: `CLOSE = { ${ this.CLOSE.map(item =>{ return `S${item.identity} `})} }`,
        finishString: `ThÃªm ${smallest.map(item => { return `S${item.identity} ` })} vÃ o táº­p OPEN, thÃªm S${s.identity} vÃ o CLOSED `,
        initString: `Láº¥y S${s.identity} ra khá»�i OPEN`
      }
      this.steps.push(stepForSave);
      console.log(stepForSave);
      i--;
    }
    console.log(this.OPEN);
    console.log(this.CLOSE);
  }

  checkSuccess(goal): boolean {
    let current = this.CLOSE[this.CLOSE.length - 1].matrix;
    return this.compareMatrix(current, goal);
  }

  compareMatrix(matrix1, matrix2) {
    for (let i = 0; i < 3; i++) {
      for (let j = 0; j < 3; j++) {
        if (matrix1[i][j] != matrix2[i][j]) {
          return false;
        }
      }
    }
    return true;
  }

  compareF(a, b) {
    if (a.f > b.f)
      return 1;
    if (a.f < b.f)
      return -1;
    return 0;
  }

  calTotalCost(s: S) {
    // console.log(s);
    for (let i = 0; i < 3; i++) {
      for (let j = 0; j < 3; j++) {
        let value = s.matrix[i][j];
        let a = 0, b = 0;
        switch (value) {
          /*1,2,3
          8, 0 ,4
          7,6,5*/
          case 1:
            a = 0,
              b = 0;
            break;
          case 2:
            a = 0;
            b = 1;
            break;
          case 3:
            a = 0;
            b = 2;
            break;
          case 4:
            a = 1;
            b = 2;
            break;
          case 5:
            a = 2;
            b = 2;
            break;
          case 6:
            a = 2;
            b = 1;
            break;
          case 7:
            a = 2;
            b = 0;
            break;
          case 8:
            a = 1;
            b = 0;
            break;
          default:
            a = i;
            b = j;
            break;
        }
        /*
          1,2,3
          4,5,6
          7,8,0
        */
        /*
           case 1:
             a = 0,
               b = 0;
             break;
           case 2:
             a = 0;
             b = 1;
             break;
           case 3:
             a = 0;
             b = 2;
             break;
           case 4:
             a = 1;
             b = 0;
             break;
           case 5:
             a = 1;
             b = 1;
             break;
           case 6:
             a = 1;
             b = 2;
             break;
           case 7:
             a = 2;
             b = 0;
             break;
           case 8:
             a = 2;
             b = 1;
             break;
           default:
             a = i;
             b = j;
             break;
         }*/

        /*
        1,2,3
        4,0,5
        6,7,8
        */
        // switch (value) {
        //   case 1:
        //     a = 0,
        //       b = 0;
        //     break;
        //   case 2:
        //     a = 0;
        //     b = 1;
        //     break;
        //   case 3:
        //     a = 0;
        //     b = 2;
        //     break;
        //   case 4:
        //     a = 1;
        //     b = 0;
        //     break;
        //   case 5:
        //     a = 1;
        //     b = 2;
        //     break;
        //   case 6:
        //     a = 2;
        //     b = 0;
        //     break;
        //   case 7:
        //     a = 2;
        //     b = 1;
        //     break;
        //   case 8:
        //     a = 2;
        //     b = 2;
        //     break;
        //   default:
        //     a = 1;
        //     b = 1;
        //     break;
        // }
        s.h += this.calCost(i, j, a, b);
        // console.log(value, 'ij:', i, j, 'ab: ', a, b, ' h:', s.h);

      }
    }
    return s.h
  }

  calCost(a, b, x, y) {

    if (a == x) {
      if (b == y + 1 || b == y - 1)
        return 1;
      if (b == y + 2 || b == y - 2)
        return 2;
      if (b == y)
        return 0;
    }
    if (a == x + 1 || a == x - 1) {
      if (b == y)
        return 1;
      if (b == y + 1 || b == y - 1)
        return 2;
      if (b == y + 2 || b == y - 2)
        return 3;
    }

    if (b == x + 2 || a == x - 2) {
      if (b == y)
        return 2
      if (b == y + 1 || b == y - 1)
        return 3
      if (b == y + 2 || b == y - 2)
        return 4
    }

    return 0;
  }

  createNewS(beginState: S, identity, i, j, a, b): S {
    let m = [[...beginState.matrix[0]],
    [...beginState.matrix[1]],
    [...beginState.matrix[2]]];
    this.switch(m, i, j, a, b);
    let newS: S = {
      matrix: null,
      g: 0,
      h: 0,
      f: 0,
      identity,
      oldIndetity: beginState.oldIndetity
    };
    newS.matrix = m;
    newS.g = beginState.g + 1;
    newS.h = this.calTotalCost(newS);
    newS.f = newS.g + newS.h;
    return newS;
  }

  movation(s: S, older: S) {
    // console.log(older);
    for (let i = 0; i < 3; i++) {
      for (let j = 0; j < 3; j++) {
        if (s.matrix[i][j] == 0) {
          // let smalest: S = {
          //   f: Number.MAX_SAFE_INTEGER,
          //   g: 0,
          //   h: 0,
          //   matrix: null,
          //   identity: s+1,
          //   oldIndetity: older.identity
          // }
          let Ss = [];
          if (i + 1 < 3) {
            const newS = this.createNewS(s, this.identity + 1, i, j, i + 1, j);
            if (!this.compareMatrix(newS.matrix, s.matrix)
              && !this.compareMatrix(newS.matrix, older.matrix)) {
              // if (this.compareF(newS, smalest) == -1) {
              // smalest = newS;
              this.identity++;
              Ss.push(newS);
              // }
            }
          }
          if (i - 1 >= 0) {
            const newS = this.createNewS(s, this.identity + 1, i, j, i - 1, j);
            if (!this.compareMatrix(newS.matrix, s.matrix)
              && !this.compareMatrix(newS.matrix, older.matrix)) {
              // if (this.compareF(newS, smalest) == -1) {
              // smalest = newS;
              // console.log('small', newS);
              this.identity++;
              Ss.push(newS);
              // }
            }
          }
          if (j + 1 < 3) {
            const newS = this.createNewS(s, this.identity + 1, i, j, i, j + 1);
            if (!this.compareMatrix(newS.matrix, s.matrix)
              && !this.compareMatrix(newS.matrix, older.matrix)) {
              // if (this.compareF(newS, smalest) == -1) {
              // smalest = newS;
              // console.log('small', newS);
              this.identity++;
              Ss.push(newS);
              // }
            }
          }
          if (j - 1 >= 0) {
            const newS = this.createNewS(s, this.identity + 1, i, j, i, j - 1);
            if (!this.compareMatrix(newS.matrix, s.matrix)
              && !this.compareMatrix(newS.matrix, older.matrix)) {
              // if (this.compareF(newS, smalest) == -1) {
              // smalest = newS;
              // console.log('small', newS);
              this.identity++;
              Ss.push(newS);
              // }
            }
          }
          // console.log("smalest", smalest);
          return Ss;
        }
      }
    }
    return null;
  }

  switch(matrix, i, j, k, l) {
    let temp = matrix[k][l];
    matrix[k][l] = 0;
    matrix[i][j] = temp;
  }
}
/*
        <div *ngFor="let step of steps" class="step row">
            {{step.initString}}
            <div class="col-12">
                <div class="row">
                    <div *ngFor="let state of step.s" class="col-3">
                        <table class="table">
                            <tbody class="table-body">
                                <tr *ngFor="let row of state.matrix">
                                    <td class="table-th">
                                        <div class="table-cell-inner">
                                            {{row[0]==0?' ':row[0]}}
                                        </div>
                                    </td>
                                    <td class="table-th">
                                        <div class="table-cell-inner">
                                            {{row[1]==0?' ':row[1]}}
                                        </div>
                                    </td>
                                    <td class="table-th">
                                        <div class="table-cell-inner">
                                            {{row[2]==0?' ':row[2]}}
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <h5 class="state"> Tráº¡ng thÃ¡i S{{state.identity}} </h5>
                        <span> TÃ­nh cÃ¡c giÃ¡ trá»‹ cá»§a S{{state.identity}} </span>
                        <li> g(S{{state.identity}}) = {{state.g}}</li>
                        <li> h(S{{state.identity}}) = {{state.h}}</li>
                        <li> f(S{{state.identity}}) = {{state.f}}</li>
                    </div>
                </div>
                <div>
                    {{step.finishString}}
                </div>
                <div>
                    {{step.OPEN}}
                </div>
                <div>
                    {{step.CLOSED}}
                </div>
            </div>
        </div>
*/