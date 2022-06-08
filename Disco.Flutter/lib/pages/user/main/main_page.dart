import 'package:disco_app/core/widgets/post/post.dart';
import 'package:disco_app/core/widgets/unicorn_image.dart';
import 'package:disco_app/pages/user/main/bloc/main_bloc.dart';
import 'package:disco_app/pages/user/main/bloc/main_event.dart';
import 'package:disco_app/pages/user/main/bloc/main_state.dart';
import 'package:disco_app/pages/user/main/bloc/stories_bloc.dart';
import 'package:disco_app/pages/user/main/bloc/stories_state.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:pull_to_refresh/pull_to_refresh.dart';

import 'bloc/stories_event.dart';

const String title = 'Your story';

class MainPage extends StatefulWidget {
  MainPage({Key? key, this.shouldLoadData = true}) : super(key: key);
  final bool shouldLoadData;

  @override
  State<MainPage> createState() => _MainPageState();
}

class _MainPageState extends State<MainPage> {
  final RefreshController _refreshController = RefreshController(initialRefresh: false);

  @override
  void initState() {
    if (widget.shouldLoadData) {
      context.read<StoriesBloc>().add(LoadStoriesEvent());
      context.read<MainPageBloc>().add(LoadPostsEvent(
            hasLoading: true,
            onLoaded: () {},
          ));
    }
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color(0xff1C142E),
      appBar: AppBar(
        backgroundColor: const Color(0xFF1C142D),
        centerTitle: false,
        title: const Text(
          "DISCO",
          style: TextStyle(
            fontSize: 32,
            fontFamily: 'Colonna',
            fontWeight: FontWeight.bold,
          ),
          textAlign: TextAlign.start,
        ),
        automaticallyImplyLeading: false,
        actions: [
          IconButton(
              padding: const EdgeInsets.only(right: 32),
              onPressed: () {
                /// context.read<MainPageBloc>().add(InitialEvent(id: 1));
              },
              icon: SvgPicture.asset(
                "assets/ic_search.svg",
                width: 32,
                height: 30,
              )),
        ],
      ),
      body: _SuccessStateWidget(
        controller: _refreshController,
      ),
    );
  }
}

// void _blocLisener(BuildContext context, Object? state) {
//   WidgetsBinding.instance?.addPostFrameCallback((timeStamp) {
//     if (state is SuccessState) {}
//   });
// }

// onSearch() {}

class _SuccessStateWidget extends StatelessWidget {
  final RefreshController controller;

  const _SuccessStateWidget({Key? key, required this.controller}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        backgroundColor: const Color(0xff1C142E),
        body: Column(
          children: [
            const SizedBox(height: 6),
            BlocBuilder<StoriesBloc, StoriesState>(
              builder: (context, state) {
                if (state is SuccessStoriesState) {
                  if (state.stories.isNotEmpty) {
                    return SizedBox(
                        height: 110,
                        child: ListView.builder(
                            scrollDirection: Axis.horizontal,
                            itemCount: state.stories.length,
                            itemBuilder: (context, index) {
                              if (index == 0) {
                                return Padding(
                                  padding: const EdgeInsets.only(left: 12, right: 8),
                                  child: UnicornImage(
                                    title: "Your story",
                                    imageUrl: state.userImageUrl,
                                    shouldHaveGradientBorder: false,
                                    shouldHavePlus: true,
                                  ),
                                );
                              } else {
                                return Padding(
                                  padding: const EdgeInsets.symmetric(horizontal: 8),
                                  child: UnicornImage(
                                    imageUrl: state.stories[index].profile?.photo ??
                                        "assets/ic_photo.png",
                                    title: state.stories[index].profile?.user?.userName ?? "",
                                  ),
                                );
                              }
                            }));
                  } else {
                    return Padding(
                      padding: const EdgeInsets.only(right: 8, top: 8),
                      child: Row(
                        children: [
                          const SizedBox(
                            width: 12,
                          ),
                          UnicornImage(
                            title: "Your story",
                            imageUrl: state.userImageUrl,
                            shouldHaveGradientBorder: false,
                            shouldHavePlus: true,
                          ),
                        ],
                      ),
                    );
                  }
                }
                return const SizedBox();
              },
            ),
            const SizedBox(height: 11),
            BlocBuilder<MainPageBloc, MainPageState>(
              builder: (context, state) {
                if (state is SuccessPostsState) {
                  return Expanded(
                    child: SmartRefresher(
                      controller: controller,
                      onRefresh: () {
                        context.read<MainPageBloc>().add(LoadPostsEvent(
                              hasLoading: false,
                              onLoaded: () {
                                controller.refreshCompleted();
                              },
                            ));
                      },
                      footer: Container(
                        color: Colors.red,
                      ),
                      child: state.posts.isNotEmpty
                          ? ListView.builder(
                              itemCount: state.posts.length,
                              itemBuilder: (context, index) {
                                if (index == state.posts.length - 1) {
                                  return Padding(
                                    padding: const EdgeInsets.only(bottom: 100.0),
                                    child: UnicornPost(post: state.posts[index]),
                                  );
                                }
                                return UnicornPost(post: state.posts[index]);
                              })
                          : const Padding(
                              padding: EdgeInsets.symmetric(horizontal: 80, vertical: 200),
                              child: Text(
                                'You don\'t have posts yet',
                                style: TextStyle(
                                  fontSize: 20,
                                  color: Colors.white,
                                ),
                              ),
                            ),
                    ),
                  );
                }
                return const Center(child: CircularProgressIndicator());
              },
            ),
            const SizedBox(height: 80.0),
          ],
        ));
  }
}
