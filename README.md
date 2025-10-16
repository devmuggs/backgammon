# Backgammon 3D (Godot + C#)

As a professional fullstack software engineer, I wanted to explore game development to bring my experience to innovative and novel mechanics and gameplay. This project is a simple 3D Backgammon game built with Godot Engine and C# to learn the engine and C# development.

プロのフルスタックソフトウェアエンジニアとして、革新的で新しいメカニクスとゲームプレイに自分の経験を活かすためにゲーム開発を探求したいと考えました。このプロジェクトは、Godot Engine と C# を使用して構築されたシンプルな 3D バックギャモンゲームで、エンジンと C# 開発を学ぶためのものです。

## Why Open Source / なぜオープンソースにしたか

I decided to make this project open source to:

- Get feedback from peers more easily.
- Share how I tackled development challenges in Godot with other developers.
- Provide reusable, educational examples for anyone learning Godot + C#.

このプロジェクトをオープンソースにした理由は以下の通りです

- 同僚や仲間からフィードバックをもらいやすくするため
- Godot での開発上の課題の解決方法を共有するため
- Godot + C# を学ぶ人向けに再利用可能な教育例を提供するため

## Why C# / なぜ C# なのか

My experience is mostly in web and enterprise applications using languages like C#, TypeScript and Python, and have a strong preference for statically typed languages or at least those with good type hinting. I chose C# for this project because of my familiarity with the language and its strong typing, which I find beneficial for larger projects.

私の経験は主に C#、TypeScript、Python などの言語を使用した Web およびエンタープライズアプリケーションにあり、静的型付け言語、または少なくとも型ヒントが優れている言語を強く好みます。このプロジェクトには、言語に精通しており、その強力な型付けが大規模なプロジェクトに有益であると感じているため、C# を選択しました。

## Future Plans / 今後の計画

Once the project is complete, I plan to publish it on itch.io free of charge, so anyone can play and explore the game.

プロジェクト完成後、itch.io に無料で公開し、誰でも遊んだり中身を覗けるようにする予定です。

## Features / 特徴

- Modular, reusable 3D components (board, pieces, dice).
- XML-documented C# scripts (bilingual English / Japanese).
- モジュール化された再利用可能な 3D コンポーネント（ボード、駒、サイコロ）
- XML コメント付き C# スクリプト（英日両対応）

## Project Structure / プロジェクト構成

```
backgammon/
├── components/        # Node3D game components
├── documentation/     # Development notes and design docs
├── scenes/            # Main and test scenes
├── scripts/           # C# scripts
│   ├── extensions/    # Node3D extension methods
│   └── utils/
├── icon.svg
├── project.godot
├── backgammon.sln
├── backgammon.csproj
├── README.md
└── LICENSE
```

## Code Structure Insights / コード構造の洞察

Although I have seen many Godot projects recommend placing all scripts in /scripts, components in /scenes, and assets in /assets, I prefer a more modular approach where each component has its own folder containing its scene and script. This structure makes it easier to manage and reuse components across different projects.

Additionally it means that if I want to edit `piece` as a feature, I can just open that folder and have everything I need in one place.

This modular structure also promotes better organization and separation of concerns, making it easier to understand and maintain the codebase (in my opinion).

見たことのある多くの Godot プロジェクトでは、すべてのスクリプトを /scripts に、コンポーネントを /scenes に、アセットを /assets に配置することが推奨されていますが、私は各コンポーネントがそのシーンとスクリプトを含む独自のフォルダーを持つ、よりモジュール化されたアプローチを好みます。この構造により、さまざまなプロジェクトでコンポーネントを管理および再利用しやすくなります。

さらに、`piece` を機能として編集したい場合、そのフォルダーを開くだけで必要なものがすべて一箇所に揃うため便利です。

このモジュール化された構造は、関心の分離とより良い組織化も促進し、コードベースを理解しやすく、維持しやすくします（私の意見では）。

- `components/` - Contains reusable 3D components like the board, pieces, and dice. Each component is a separate scene with its own script.
- `documentation/` - Holds development notes and design documents to track progress and ideas.
- `scripts/extensions/` - Contains extension methods for Node3D and other Godot classes to add custom functionality.

## Getting Started / はじめに

1. Clone the repository / リポジトリをクローンする

   ```bash
   git clone https://github.com/devmuggs/backgammon.git
   ```

2. Open the project in Godot 4 / Godot 4 でプロジェクトを開く
3. Run the `main.tscn` scene to start the game / `main.tscn` シーンを実行してゲームを開始する

## Requirements / 必要条件

- Godot Engine 4.x
- .NET 7+ SDK
- Optional: Visual Studio Code / Visual Studio 2022+ / Rider (for C# editing)

## License / ライセンス

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
このプロジェクトは MIT ライセンスの下でライセンスされています。詳細は [LICENSE](LICENSE) ファイルを参照してください。

## Contributions / 貢献

Contributions are welcome! Please fork the repository and submit a pull request with your changes.
貢献は歓迎します！リポジトリをフォークし、変更内容を含むプルリクエストを送信してください。
