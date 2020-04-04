using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // player関連
    public int playerNum;       // player番号
    float size = 1.0f;          // １マスのサイズ
    float speed = 10f;           // playerの速度
    Transform myTransform;      // transformのキャッシュ先
    Vector3 target;             // playerの移動先
    Vector3 prePos;             // playerの一つ前の位置
    Vector3 movement;           // playerの移動方向

    // Ray関連
    Ray rayToForward;           // 移動先へのray
    int wallLayer = 1 << 10 | 1 << 11;
    Ray rayToSeed;              // タネ探索用のray
    int seedLayer = 1 << 9;     // タネ系オブジェクトのレイヤー
    // enemy関連
    int enemyNum;               // 相手のplayer番号

    // タネ関連
    public Transform[] seedPrefabs;    // タネのプレハブ
    public int chosenOption = 0;       // 選択中のタネ番号
    public int[] seedCounts;           // それぞれのタネの所持数

    // 得点
    public int score = 0;


    void Start () {
        // playerの移動関連
        myTransform = transform;
        target = myTransform.position;
        movement.Set(0, 0, 0);

        // enemyの番号
        enemyNum = CalcEnemyPlayerNumber(playerNum);

    }


    void Update()
    {
        if (MainController.state == 2) {
            // playerが移動済みかどうか
            if (myTransform.position == target)
            {
                switch (playerNum)
                {
                    case 1: // player1の場合
                        if (Input.GetButtonDown("Abutton1")) { GetSeedKey(); }  // returnキーが押されたらタネに干渉
                        else { SetTargetPosition(); }                           // returnキーが押されていないならtargetをset
                        break;
                    case 2: // player2の場合
                        if (Input.GetButtonDown("Abutton2")) { GetSeedKey(); }  // spaceキーが押されたらタネに干渉
                        else { SetTargetPosition(); }                           // spaceキーが押されていないならtargetをset
                        break;
                    default:
                        break;
                }

            }
        }
    }


    // 秒数固定の更新
    void FixedUpdate()
    {
        // playerを動かす
        Move();
    }


    // 方向キーを押した時
    void SetTargetPosition() {
        //prePos = target;                              // 前のpositionを保存
        float h = 0f;   // 横方向の入力
        float v = 0f;   // 縦方向の入力

        switch (playerNum) {
            case 1: // player1の場合
                h = Input.GetAxis("Horizontal1");   // 横方向
                v = -Input.GetAxis("Vertical1");    // 縦方向
                break;
            case 2: // player2の場合
                h = Input.GetAxis("Horizontal2");   // 横方向
                v = -Input.GetAxis("Vertical2");    // 縦方向
                break;
            default:
                break;
        }
        movement.Set(h, 0, v);                          // h,vをmovementに代入=方向の決定
        // 壁や相手が正面にいないなら移動させる
        if (!RayToForwardd(movement)) {
            target = myTransform.position + movement * size;    // 移動先のpositionを計算
        }
    }


    // playerを動かす
    void Move() {
        myTransform.position = Vector3.MoveTowards(myTransform.position, target, speed * Time.deltaTime);
    }


    // 正面にレイを飛ばす
    bool RayToForwardd(Vector3 direction) {
        rayToForward = new Ray(myTransform.position, direction);
        RaycastHit obj;
        if (Physics.Raycast(rayToForward, out obj, 1.0f, wallLayer)) {
            return true;
        } else {
            return false;
        }
    }


    // return/spaceキーを押された時に呼ばれる関数
    void GetSeedKey() {
        Vector3 rayPosition = myTransform.position;         // rayの始点
        rayPosition.y += 5f;                                // rayの始点をplayerの真上に
        rayToSeed = new Ray(rayPosition, -myTransform.up);  // rayを作成
        RaycastHit seed;    // rayがhitしたオブジェクト
        // rayがタネ系に当たった場合
        if(Physics.Raycast(rayToSeed, out seed, 5.0f, seedLayer)) {
            Transform seedTransform = seed.transform;
            // 足元に相手のタネ系オブジェクトがある
            if (seedTransform.name == enemyNum.ToString()) {
                return;
            }
            // 足元に自分の花オブジェクトがある
            else if (seedTransform.tag == "Flower") {
                TakeSeed(seedTransform); // 花を摘む
            }
        }
        // 足元に何もオブジェクトがない
        else if(seedCounts[chosenOption] > 0) {
            SetSeed();  // タネを植える
        }

    }


    // タネを植える
    void SetSeed() {
        string seedName = playerNum.ToString();
        Vector3 position = new Vector3(myTransform.position.x, 0f, myTransform.position.z);
        Transform seed = Instantiate(seedPrefabs[chosenOption], position, Quaternion.identity);  // タネをインスタンス化
        seed.name = seedName;       // タネオブジェクトの名前 x,z
        seedCounts[chosenOption] -= 1;               // タネを消費
    }


    // タネを取る(花を摘む)
    void TakeSeed(Transform seed) {
        seed.GetComponent<FlowerController>().TakeFlower(seedCounts); // 花が摘まれた時の処理
        Destroy(seed.gameObject);                                     // 花オブジェクトを削除
    }


    // 相手のplayer番号を計算
    int CalcEnemyPlayerNumber(int n) {
        switch (n) {
            case 1:
                return 2;
            case 2:
                return 1;
            default:
                print("error with playerNum");
                break;
        }
        return 1;
    }
}
